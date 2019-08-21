using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbManager
{
    public partial class DeliveryManager
    {
        public Device GetDevice(string serialNumber, out Device device) // поиск устройства по заводскому номеру 
        {
            device = db.Devices.FirstOrDefault(d => d.SerialNumber == serialNumber);
            return device;
        }

        public Device GetDevice(string serialNumber)
        {
            Device device = db.Devices.FirstOrDefault(d => d.SerialNumber == serialNumber);
            return device;
        }

        public DeviceType GetDeviceType (string serialNumber, out DeviceType type) // поиск типа устройства по заводскому номеру
        {
            type = db.DeviceTypes.FirstOrDefault(t => t.ModelCode == serialNumber.Substring(0, 6));
            return type;
        }

        public DeviceType GetDeviceType(string serialNumber)
        {
            DeviceType type = db.DeviceTypes.FirstOrDefault(t => t.ModelCode == serialNumber.Substring(0, 6));
            return type;
        }

        private string DeviceDeliveryCheck (Device device, DeliveryAct act, string serialNumber) //проверка возможноти провести акт или добавить устройство в акт
        {
            GetDeviceType(serialNumber, out DeviceType type);
            if (type == null)
                return "Тип оборудования не найден в БД!";
            
            //if(device == null) return "Оборудование не найдено в БД!";

            if (device != null && device.CurrentState == "возврат производителю" && act.DeliveryType.Description != "прием на склад")
                return "Оборудование было возвращено производителю!";

            if (device != null && device.ContractId != act.ContractId)
                return "Оборудование [" + device.SerialNumber + "] относится к другому контракту";
           

            if (device == null && act.DeliveryType.Description != "прием на склад")
                 return "Оборудование [" + serialNumber + "] не найдено в БД";

            if(device != null && device.CurrentState == "брак" && act.DeliveryType.Description != "удаление из брака" && act.DeliveryType.Description != "возврат производителю")
                return "Оборудование [" + device.SerialNumber + "] уже имеет статус <<брак>>";

            switch (act.DeliveryType.Description)
            {
                case "прием на склад":
                    if (device != null && device.CurrentState != "возврат производителю")
                        return "Оборудование [" + device.SerialNumber + "] уже присутствует в БД!";
                    else return string.Empty;

                case "выдача со склада":
                    if (device.CurrentState == "выдача со склада" || device.CurrentState == "привязан к ту")
                        return "Оборудование [" + device.SerialNumber + "] уже имеет статус выдачи со склада";
                    else return string.Empty;

                case "брак":
                    if (device.CurrentState == "брак")
                        return "Оборудование [" + device.SerialNumber + "] уже имеет статус <<брак>>";
                    if (device.CurrentState == "выдача со склада" || device.CurrentState == "привязан к ту")
                        return "Оборудование [" + device.SerialNumber + "] должно быть возвращено на склда до занесения в брак";
                    else return string.Empty;

                case "выдача на сборку":
                    if (device.CurrentState == "выдача на сборку")
                        return "Оборудование [" + device.SerialNumber + "] уже имеет статус <<выдача на сборку>>";
                    else return string.Empty;

                case "выдача на настройку":
                    if (device.CurrentState == "выдача на настройку")
                        return "Оборудование [" + device.SerialNumber + "] уже имеет статус <<выдача на настройку>>";
                    else return string.Empty;

                case "возврат со сборки":
                    return string.Empty;

                case "возврат с настройки":
                    return string.Empty;

                case "возврат на склад":
                    if (!(device.CurrentState == "выдача со склада" || device.CurrentState == "пивязан к ТУ" || device.CurrentState == "включен в отчет" || device.CurrentState == "принят куратором"))
                        return "Оборудование [" + device.SerialNumber + "] не имеет статус выдачи со склада";
                    else return string.Empty;

                case "возврат производителю":
                    if(device.CurrentState == "возврат производителю")
                        return "Оборудование [" + device.SerialNumber + "] уже имеет статус возврата производителю";
                    if (device.CurrentState != "прием на склад" && device.CurrentState != "брак")
                        return "Оборудование должно находится на складе! Текущий статус <<" + device.CurrentState + ">>";
                    else return string.Empty;

                default:
                    return "Неизвестная операция";


            }
        }

        public string NewDeviceDelivery (string serialNumber, int deliveryActId, string comment="") // Добавление устройства в акт
        {
            string error = string.Empty;
            if(GetDeliveryAct(deliveryActId, out DeliveryAct act) != null && GetDeviceType(serialNumber, out DeviceType type) != null)
            {
                foreach (var item in act.DeviceDeliveries)
                {
                    if (serialNumber == item.SerialNumber) return "Это оборудование уже есть в таблице!";
                }
                error = DeviceDeliveryCheck(GetDevice(serialNumber), act, serialNumber);
               
                if (error == string.Empty)
                {
                    try
                    {
                        DeviceDelivery deviceDelivery = new DeviceDelivery()
                        {
                            SerialNumber = serialNumber,
                            DeliveryActId = act.Id,
                            DeviceTypeId = type.Id,
                            Comment = comment
                        };
                        db.DeviceDeliveries.Add(deviceDelivery);
                        db.SaveChanges();
                        return string.Empty;
                    }
                    catch
                    {
                        return error;
                    }
                }
                return error;
            }
            return "Тип оборудования не найден в БД";
        }

        public string NewDeviceDelivery (string serialNumber, DeliveryAct act, string comment = "") // Добавление устройства в акт
        {
            string error = string.Empty;
            if (act == null) return "Невозможно добавить запись в этот акт!";
            if (GetDeviceType(serialNumber, out DeviceType type) == null) return "Тип оборудования не найден в БД!";
            foreach (var item in act.DeviceDeliveries)
            {
                if (serialNumber == item.SerialNumber) return "Это оборудование уже есть в таблице!";
            }
            error = DeviceDeliveryCheck(GetDevice(serialNumber), act, serialNumber);
           
            if (error == string.Empty)
            {
                try
                {
                    DeviceDelivery deviceDelivery = new DeviceDelivery()
                    {
                        SerialNumber = serialNumber,
                        DeliveryActId = act.Id,
                        DeviceTypeId = type.Id,
                        Comment = comment
                    };
                    
                    db.DeviceDeliveries.Add(deviceDelivery);
                    db.SaveChanges();
                    return string.Empty;
                }
                catch
                {
                    return error;
                }
            }
            return error;
        }

        public string NewDeviceDelivery(string serialNumber, DeliveryAct act, int setId, string comment = "") // Добавление устройства в акт с привязкой комплекта материалов
        {
            string error = string.Empty;
            if (act == null) return "Невозможно добавить запись в этот акт!";
            if (GetDeviceType(serialNumber, out DeviceType type) == null) return "Тип оборудования не найден в БД!";
            foreach (var item in act.DeviceDeliveries)
            {
                if (serialNumber == item.SerialNumber) return "Это оборудование уже есть в таблице!";
            }
            error = DeviceDeliveryCheck(GetDevice(serialNumber), act, serialNumber);

            if (error == string.Empty)
            {
                try
                {
                    DeviceDelivery deviceDelivery = new DeviceDelivery()
                    {
                        SerialNumber = serialNumber,
                        DeliveryActId = act.Id,
                        DeviceTypeId = type.Id,
                        Comment = comment
                    };
                    if(setId != 0)
                        deviceDelivery.SetId = setId;                     
                    db.DeviceDeliveries.Add(deviceDelivery);

                    if(act.DeliveryType.Description == "выдача со склада")
                    {
                        Device device = db.Devices.FirstOrDefault(d => d.SerialNumber == serialNumber);
                        if(device != null && device.SetId != 0 && device.SetId != null)
                            AddSetToAct(act.Id, (int)device.SetId);
                    }
                    db.SaveChanges();
                    return string.Empty;
                }
                catch
                {
                    return error;
                }
            }
            return error;
        }

        public string DeleteDeviceDelivery (int deliveryId)
        {
            DeviceDelivery delivery = db.DeviceDeliveries.Find(deliveryId);
            if(delivery != null)
            {
                try
                {
                    db.DeviceDeliveries.Remove(delivery);
                    db.SaveChanges();
                    return string.Empty;
                }
                catch
                {
                    return "Невозможно удалить!";
                }
            }
            else return "Невозможно удалить!";
        }
    }
}
