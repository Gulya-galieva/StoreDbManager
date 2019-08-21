using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbManager
{
    public partial class DeliveryManager
    {
        StoreContext db;

        public DeliveryManager()
        {
            db = new StoreContext();
        }

        public DeliveryAct GetDeliveryAct(int id, out DeliveryAct act) //Поиск акта по id
        {
            act = db.DeliveryActs.Find(id);
            return act;
        }

        public DeliveryAct GetDeliveryAct(int id)
        {
            DeliveryAct act = db.DeliveryActs.Find(id);
            return act;
        }

        public IEnumerable<DeliveryAct> GetDeliveryActs(int contractId, string deliveryState, string deliveryType)
        {
            List<DeliveryAct> result = new List<DeliveryAct>();
            GetContract(contractId, out Contract contract);
            if (contract != null)
                result = db.DeliveryActs.Where(a => a.ContractId == contract.Id && a.DeliveryState.Description == deliveryState && a.DeliveryType.Description == deliveryType).ToList();
            return result;
        }

        public IEnumerable<DeliveryAct> GetDeliveryActs(Contract contract, string deliveryState, string deliveryType)
        {
            List<DeliveryAct> result = new List<DeliveryAct>();
            if (contract != null)
                result = db.DeliveryActs.Where(a => a.ContractId == contract.Id && a.DeliveryState.Description == deliveryState && a.DeliveryType.Description == deliveryType).ToList();
            return result;
        }

        public DeliveryType GetDeliveryType(string typeDesc, out DeliveryType type) //Поиск типа акта по описанию 
        {
            type = db.DeliveryTypes.FirstOrDefault(t => t.Description == typeDesc);
            return type;
        }

        public DeliveryType GetDeliveryType(string typeDesc)
        {
            DeliveryType type = db.DeliveryTypes.FirstOrDefault(t => t.Description == typeDesc);
            return type;
        }

        public DeliveryState GetDeliveryState(string stateDesc, out DeliveryState state) // Поиск типа состояния акта по описанию 
        {
            state = db.DeliveryStates.FirstOrDefault(s => s.Description == stateDesc);
            return state;
        }

        public DeliveryState GetDeliveryState(string stateDesc)
        {
            DeliveryState state = db.DeliveryStates.FirstOrDefault(s => s.Description == stateDesc);
            return state;
        }

        public Worker GetWorker(int id, out Worker worker) // Поиск работника 
        {
            worker = db.Workers.Find(id);
            return worker;
        }

        public Worker GetWorker(int id)
        {
            return db.Workers.Find(id);
        }

        public Contract GetContract(int id, out Contract contract) //Поиск контракта 
        {
            contract = db.Contracts.Find(id);
            return contract;
        }

        public Contract GetContract(int id)
        {
            return db.Contracts.Find(id);
        }

        public int GetUserId(string login)
        {
            User user = db.Users.FirstOrDefault(u => u.Login == login);
            if (user != null)
                return user.Id;
            else return 0;
        }

        public int GetUserId(string login, out int id)
        {
            User user = db.Users.FirstOrDefault(u => u.Login == login);
            if (user != null)
            {
                id = user.Id;
                return id;
            }
            else
            {
                id = 0;
                return id;
            }
        }

        public string NewDeliveryAct(string typeDesc, int userId, int contractId) // создание нового акта 
        {
            if (GetDeliveryType(typeDesc, out DeliveryType type) != null && GetDeliveryState("в работе", out DeliveryState state) != null)
            {
                try
                {
                    DeliveryAct act = new DeliveryAct
                    {
                        Date = DateTime.Now,
                        DeliveryTypeId = type.Id,
                        DeliveryStateId = state.Id,
                        UserId = userId
                    };
                    if (contractId == 0)
                        act.ContractId = null;
                    else
                        act.ContractId = contractId;
                    db.DeliveryActs.Add(act);
                    db.SaveChanges();
                    return act.Id.ToString();
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else return "Не возможно создать акт!";
        }

        public string DeleteDeliveryAct(int id) // удаление акта 
        {
            string error = "Невозможно удалить акт!";
            if (GetDeliveryAct(id, out DeliveryAct act) != null)
            {
                try
                {
                    db.DeliveryActs.Remove(act);
                    db.SaveChanges();
                    return string.Empty;
                }
                catch
                {
                    return error;
                }
            }
            else return error;
        }

        public string AddMaterialToAct(DeliveryAct act, MaterialDelivery delivery)
        {
            bool add = false;
            double volume = 0;
            if (act.DeliveryType.Description == "выдача со склада")
                volume = 0 - delivery.Volume;
            if (act != null && delivery != null)
            {
                foreach (var materialDelivery in act.MaterialDeliveries)
                {
                    if (materialDelivery.MaterialTypeId == delivery.MaterialTypeId)
                    {
                        materialDelivery.Volume += volume;
                        add = true;
                    }
                }
                if (!add)
                {
                    db.MaterialDeliveries.Add(new MaterialDelivery { DeliveryActId = act.Id, MaterialId = delivery.MaterialId, MaterialTypeId = delivery.MaterialTypeId, Volume = volume, Other = delivery.Other });
                }
                db.SaveChanges();
                return "";
            }
            else
                return "Невозможно добавить материалы в акт!";
        }

        public string AddSetToAct(int actId, int setId)
        {
            DeliveryAct act = db.DeliveryActs.Find(actId);
            DeliveryAct set = db.DeliveryActs.Find(setId);
            string error = "";
            if (set != null)
            {
                foreach (var materialDelivery in set.MaterialDeliveries)
                {
                    error = AddMaterialToAct(act, materialDelivery);
                    if (error != "")
                        return error;
                }
            }
            return error;
        }

        #region ProcessDelivery

        public string ProcessDeliveryAct(int deliveryActId, int? workerId, string setName, int userId)  // определение типа акта для дальнейшей обработки 
        {
            string result;
            GetDeliveryAct(deliveryActId, out DeliveryAct act);
            if (act != null)
            {
                act.UserId = userId;
                if (act.DeliveryState.Description != "в работе")
                    return "Акт уже был проведен!";
                switch (act.DeliveryType.Description)
                {
                    case "прием на склад":
                        result = ProcessIncomeDeliveryAct(act, userId);
                        return result;

                    case "выдача со склада":
                        result = ProcessOutcomeDeliveryAct(act, (int)workerId, userId);
                        return result;

                    case "возврат на склад":
                        result = ProcessReturnDeliveryAct(act, (int)workerId, userId);
                        return result;

                    case "комплект":
                        result = ProcessSetDeliveryAct(act, setName);
                        return result;

                    case "выдача на сборку":
                        result = ProcessToAssemblyDeliveryAct(act, (int)workerId, userId);
                        return result;

                    case "возврат со сборки":
                        result = ProcessFromAssemblyDeliveryAct(act, userId);
                        return result;

                    case "выдача на настройку":
                        result = ProcessToTuneDeliveryAct(act, (int)workerId, userId);
                        return result;

                    case "возврат с настройки":
                        result = ProcessFromTuneDeliveryAct(act, userId);
                        return result;

                    case "прием инструмента и сиз":
                        result = ProcessPPEIncome(act);
                        return result;

                    case "выдача инструмента и сиз":
                        result = ProcessPPEOutcome(act, (int)workerId);
                        return result;

                    case "возврат производителю":
                        result = ProcessReturnToMnfcAct(act, userId);
                        return result;

                    case "брак":
                        result = ProcessDefectAct(act, userId);
                        return result;

                    default:
                        return "Неопределенная операция!";
                }
            }
            else return "невозможно обработать акт";
        }

        private string ProcessIncomeDeliveryAct(DeliveryAct act, int userId) // обработка акта приема 
        {
            string error = string.Empty;
            foreach (var deviceDelivery in act.DeviceDeliveries)
            {
                GetDevice(deviceDelivery.SerialNumber, out Device tmpDevice);
                if (tmpDevice == null)
                {
                    error = DeviceDeliveryCheck(GetDevice(deviceDelivery.SerialNumber), act, deviceDelivery.SerialNumber);
                    if (error == string.Empty)
                    {
                        Device device = new Device { SerialNumber = deviceDelivery.SerialNumber, ContractId = (int)deviceDelivery.DeliveryAct.ContractId, DeviceTypeId = GetDeviceType(deviceDelivery.SerialNumber).Id, CurrentState = "прием на склад" };
                        db.Devices.Add(device);
                        db.SaveChanges();
                        deviceDelivery.DeviceId = device.Id;
                        if (AddDeviceState("прием на склад", userId, device.Id)) db.SaveChanges();
                        else return "Не удалось добавить статус ПУ <<прием на склад>>!";
                    }
                    else return error;
                }
                else
                {
                    error = DeviceDeliveryCheck(GetDevice(deviceDelivery.SerialNumber), act, deviceDelivery.SerialNumber);
                    if (error == string.Empty)
                    {
                        tmpDevice.AddState(DeviceStateTypeName.Income, userId, null);
                        db.SaveChanges();
                    }
                    else return error;
                }
            }
            foreach (var materialDelivery in act.MaterialDeliveries)
            {
                Material material = db.Materials.FirstOrDefault(m => m.ContractId == materialDelivery.DeliveryAct.ContractId && m.MaterialTypeId == materialDelivery.MaterialTypeId);
                if (material == null)
                {
                    material = new Material { MaterialTypeId = materialDelivery.MaterialTypeId, ContractId = materialDelivery.DeliveryAct.ContractId, Volume = materialDelivery.Volume };
                    db.Materials.Add(material);
                    db.SaveChanges();
                    materialDelivery.MaterialId = material.Id;
                }
                else
                {
                    material.Volume += materialDelivery.Volume;
                    materialDelivery.MaterialId = material.Id;
                }
            }
            act.DeliveryStateId = GetDeliveryState("проведен").Id;
            db.SaveChanges();
            return string.Empty;
            
        }

        private string ProcessOutcomeDeliveryAct(DeliveryAct act, int workerId, int userId) // обработка акта выдачи 
        {
            string error = string.Empty;

            GetWorker(workerId, out Worker worker);
            if (worker == null)
            {
                return "Работник не выбран!";
            }

            if (!worker.DeliveryAvailible)
                return "Выдача для " + worker.Surname + " " + worker.Name + " " + worker.MIddlename + " заблокирована!";

            foreach (var deviceDelivery in act.DeviceDeliveries)
            {
                error = DeviceDeliveryCheck(GetDevice(deviceDelivery.SerialNumber, out Device device), act, deviceDelivery.SerialNumber);
                if (error == string.Empty)
                {
                    device.CurrentState = "выдача со склада";
                    deviceDelivery.DeviceId = device.Id;
                    if (AddDeviceState("выдача со склада", userId, device.Id)) db.SaveChanges();
                    else return "Не удалось добавить статус ПУ <<выдача со склада>>!";
                }
                else return error;
            }
            foreach (var materialDelivery in act.MaterialDeliveries)
            {
                Material material = db.Materials.FirstOrDefault(m => m.ContractId == materialDelivery.DeliveryAct.ContractId && m.MaterialTypeId == materialDelivery.MaterialTypeId);
                material.Volume += materialDelivery.Volume;
                materialDelivery.MaterialId = material.Id;
            }
            act.DeliveryStateId = GetDeliveryState("проведен").Id;
            act.Date = DateTime.Now;
            act.WorkerId = workerId;
            db.SaveChanges();
            return string.Empty;
        }

        private string ProcessReturnDeliveryAct(DeliveryAct act, int workerId, int userId) // обработка акта возврата на склад 
        {
            string error = string.Empty;
            foreach (var deviceDelivery in act.DeviceDeliveries)
            {
                error = DeviceDeliveryCheck(GetDevice(deviceDelivery.SerialNumber, out Device device), act, deviceDelivery.SerialNumber);
                if (error == string.Empty)
                {
                    device.CurrentState = "прием на склад";
                    deviceDelivery.DeviceId = device.Id;
                    if (AddDeviceState("возврат на склад", userId, device.Id)) db.SaveChanges();
                    else return "Не удалось добавить статус ПУ <<возврат на склад>>!";
                }
                else return error;
            }
            foreach (var materialDelivery in act.MaterialDeliveries)
            {
                Material material = db.Materials.FirstOrDefault(m => m.ContractId == materialDelivery.DeliveryAct.ContractId && m.MaterialTypeId == materialDelivery.MaterialTypeId);
                material.Volume += materialDelivery.Volume;
                materialDelivery.MaterialId = material.Id;
            }
            act.DeliveryStateId = GetDeliveryState("проведен").Id;
            act.WorkerId = workerId;
            act.Date = DateTime.Now;
            db.SaveChanges();
            return string.Empty;
        }

        private string ProcessSetDeliveryAct(DeliveryAct act, string setName) //сохранение комплекта 
        {
            try
            {
                act.SetName = setName;
                db.SaveChanges();
                return string.Empty;
            }
            catch
            {
                return "Невозможно сохранить комплект";
            }
        }

        private string ProcessToAssemblyDeliveryAct(DeliveryAct act, int workerId, int userId) // обработка акта выдачи на сборку 
        {
            string error = string.Empty;
            foreach (var deviceDelivery in act.DeviceDeliveries)
            {
                error = DeviceDeliveryCheck(GetDevice(deviceDelivery.SerialNumber, out Device device), act, deviceDelivery.SerialNumber);
                if (error == string.Empty)
                {
                    device.CurrentState = "выдача на сборку";
                    deviceDelivery.DeviceId = device.Id;
                    if (deviceDelivery.SetId != 0)
                        device.SetId = deviceDelivery.SetId;
                    if (AddDeviceState("выдача на сборку", userId, device.Id)) db.SaveChanges();
                    else return "Не удалось добавить статус ПУ <<выдача на сборку>>!";
                }
                else return error;
            }
            act.DeliveryStateId = GetDeliveryState("проведен").Id;
            act.Date = DateTime.Now;
            act.WorkerId = workerId;
            db.SaveChanges();
            return string.Empty;
        }

        private string ProcessFromAssemblyDeliveryAct(DeliveryAct act, int userId) // обработка акта возврата со сборки 
        {
            string error = string.Empty;
            foreach (var deviceDelivery in act.DeviceDeliveries)
            {
                error = DeviceDeliveryCheck(GetDevice(deviceDelivery.SerialNumber, out Device device), act, deviceDelivery.SerialNumber);
                if (error == string.Empty)
                {
                    device.CurrentState = "прием на склад";
                    deviceDelivery.DeviceId = device.Id;
                    if (AddDeviceState("возврат со сборки", userId, device.Id)) db.SaveChanges();
                    else return "Не удалось добавить статус ПУ <<возврат со сборки>>!";
                }
                else return error;
            }
            act.DeliveryStateId = GetDeliveryState("проведен").Id;
            act.Date = DateTime.Now;
            db.SaveChanges();
            return string.Empty;
        }

        private string ProcessToTuneDeliveryAct(DeliveryAct act, int workerId, int userId) // обработка акта выдачи на настройку 
        {
            string error = string.Empty;
            foreach (var deviceDelivery in act.DeviceDeliveries)
            {
                error = DeviceDeliveryCheck(GetDevice(deviceDelivery.SerialNumber, out Device device), act, deviceDelivery.SerialNumber);
                if (error == string.Empty)
                {
                    device.CurrentState = "выдача на настройку";
                    deviceDelivery.DeviceId = device.Id;
                    if (AddDeviceState("выдача на настройку", userId, device.Id)) db.SaveChanges();
                    else return "Не удалось добавить статус ПУ <<выдача на настройку>>!";
                }
                else return error;
            }
            act.DeliveryStateId = GetDeliveryState("проведен").Id;
            act.Date = DateTime.Now;
            act.WorkerId = workerId;
            db.SaveChanges();
            return string.Empty;
        }

        private string ProcessFromTuneDeliveryAct(DeliveryAct act, int userId) // обработка акта возврата с настройки 
        {
            string error = string.Empty;
            foreach (var deviceDelivery in act.DeviceDeliveries)
            {
                error = DeviceDeliveryCheck(GetDevice(deviceDelivery.SerialNumber, out Device device), act, deviceDelivery.SerialNumber);
                if (error == string.Empty)
                {
                    device.CurrentState = "прием на склад";
                    deviceDelivery.DeviceId = device.Id;
                    if (AddDeviceState("возврат с настройки", userId, device.Id)) db.SaveChanges();
                    else return "Не удалось добавить статус ПУ <<возврат с настройки>>!";
                }
                else return error;
            }
            act.DeliveryStateId = GetDeliveryState("проведен").Id;
            act.Date = DateTime.Now;
            db.SaveChanges();
            return string.Empty;
        }

        private string ProcessPPEIncome(DeliveryAct act)
        {
            string error = string.Empty;
            foreach (var item in act.PPEDeliveries)
            {
                PPE ppe = db.PPEs.Find(item.PPEId);
                if (ppe != null)
                {
                    ppe.Volume += item.Volume;
                    act.DeliveryStateId = GetDeliveryState("проведен").Id;
                    db.SaveChanges();
                }
                else return "Тип не найден в БД!";
            }
            return string.Empty;

        }

        private string ProcessPPEOutcome(DeliveryAct act, int workerId)
        {
            string error = string.Empty;
            Worker worker = db.Workers.Find(workerId);
            if (worker == null)
                return "Работник не найден в БД!";
            act.WorkerId = workerId;

            foreach (var item in act.PPEDeliveries)
            {
                PPE ppe = db.PPEs.Find(item.PPEId);
                if (ppe != null)
                {
                    ppe.Volume += item.Volume;
                    act.DeliveryStateId = GetDeliveryState("проведен").Id;
                    db.SaveChanges();
                }
                else return "Тип не найден в БД!";
            }
            return string.Empty;
        }

        private string ProcessDefectAct(DeliveryAct act, int userId)
        {
            string error = string.Empty;

            foreach (var deviceDelivery in act.DeviceDeliveries)
            {
                error = DeviceDeliveryCheck(GetDevice(deviceDelivery.SerialNumber, out Device device), act, deviceDelivery.SerialNumber);
                if (error == string.Empty)
                {
                    device.CurrentState = "брак";
                    deviceDelivery.DeviceId = device.Id;
                    if (AddDeviceState("брак", userId, device.Id)) db.SaveChanges();
                    else return "Не удалось добавить статус ПУ <<брак>>!";
                }
                else return error;
            }

            act.DeliveryStateId = GetDeliveryState("проведен").Id;
            act.Date = DateTime.Now;
            act.UserId = userId;
            db.SaveChanges();
            return string.Empty;
        }

        private string ProcessReturnToMnfcAct(DeliveryAct act, int userId)
        {
            string error = string.Empty;
            foreach (var deviceDelivery in act.DeviceDeliveries)
            {
                error = DeviceDeliveryCheck(GetDevice(deviceDelivery.SerialNumber, out Device device), act, deviceDelivery.SerialNumber);
                if (error == string.Empty)
                {
                    device.CurrentState = "возврат производителю";
                    deviceDelivery.DeviceId = device.Id;
                    if (AddDeviceState("возврат производителю", userId, device.Id)) db.SaveChanges();
                    else return "Не удалось добавить статус ПУ <<возврат на склад>>!";
                }
                else return error;
            }
            foreach (var materialDelivery in act.MaterialDeliveries)
            {
                Material material = db.Materials.FirstOrDefault(m => m.ContractId == materialDelivery.DeliveryAct.ContractId && m.MaterialTypeId == materialDelivery.MaterialTypeId);
                material.Volume += materialDelivery.Volume;
                materialDelivery.MaterialId = material.Id;
            }
            act.DeliveryStateId = GetDeliveryState("проведен").Id;
            act.Date = DateTime.Now;
            db.SaveChanges();
            return string.Empty;
        }

        #endregion

        private bool AddDeviceState(string description, int userId, int deviceId)
        {
            DeviceStateType type = db.DeviceStateTypes.FirstOrDefault(t => t.Description == description);
            if (type == null) return false;
            else
            {
                db.DeviceStates.Add(new DeviceState() { DeviceId = deviceId, UserId = userId, DeviceStateTypeId = type.Id, Date = DateTime.Now });
                db.SaveChanges();
                return true;
            }
        }
}
}

