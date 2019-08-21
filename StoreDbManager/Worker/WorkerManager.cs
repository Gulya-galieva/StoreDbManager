using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbManager
{
    public class WorkerManager
    {
        StoreContext db;

        public WorkerManager( StoreContext context)
        {
            db = context;
        }

        public string GetWorkerName (Worker worker)
        {
            string result = worker.Surname + " " + worker.Name;
            if (worker.MIddlename != "" && worker.MIddlename != null)
                result += " " + worker.MIddlename;
            return result;
        }

        public string GetWorkerName(int workerId)
        {
            Worker worker = db.Workers.Find(workerId);
            return GetWorkerName(worker);
        }

        public string GetShortWorkerName(Worker worker)
        {
            string result = worker.Surname + " " + worker.Name.Substring(0,1) + ".";
            if (worker.MIddlename != "" && worker.MIddlename != null)
                result += worker.MIddlename.Substring(0,1) + ".";
            return result;
        }

        public string GetShortWorkerName(int workerId)
        {
            Worker worker = db.Workers.Find(workerId);
            return GetShortWorkerName(worker);
        }

        public List<Device> GetWorkerDevices(Worker worker)
        {
            if (worker == null) throw new Exception("Нулевая ссылка на работника в WorkerManager.GetWorkerDevices(Worker worker)");
            return GetWorkerDevices(worker.Id);
        }

        public List<Device> GetWorkerDevices(int workerId)
        {
            List<DeliveryAct> deliveryActs = db.DeliveryActs.Where(a => a.WorkerId == workerId).ToList();
            List<Device> devices = new List<Device>();

            foreach (var item in deliveryActs)
            {
                foreach (var deviceDelivery in item.DeviceDeliveries)
                {
                    Device device = deviceDelivery.Device;
                    DeviceDelivery lastDelivery = db.DeviceDeliveries.Where(d => d.DeviceId == device.Id).OrderByDescending(x => x.Id).ToList().First();
                    if (item.Id == lastDelivery.DeliveryAct.Id)
                    {
                        devices.Add(device);
                    }
                }
            }
            return devices;
        }

        public int GetWorkerDevicesCount (int workerId)
        {
            int count = 0;
            foreach (var item in db.DeliveryActs.Where(a => a.WorkerId == workerId))
            {
                foreach (var deviceDelivery in item.DeviceDeliveries)
                {
                    Device device = deviceDelivery.Device;
                    DeviceDelivery lastDelivery = db.DeviceDeliveries.Where(d => d.DeviceId == device.Id).OrderByDescending(x => x.Id).ToList().First();
                    if (item.Id == lastDelivery.DeliveryAct.Id)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        public List<Material> GetWorkerMaterials(Worker worker)
        {
            bool newMaterial = true;
            List<Material> materials = new List<Material>();
            foreach (var act in GetWorkerDeliveryActs(worker))
            {
                foreach (var materialDelivery in act.MaterialDeliveries)
                {
                    newMaterial = true;
                    foreach (var material in materials)
                    {
                        if(material.Id == materialDelivery.Material.Id)
                        {
                            newMaterial = false;
                            break;
                        }
                    }
                    if(newMaterial)
                    {
                        materials.Add(materialDelivery.Material);
                        newMaterial = false;
                    }
                }
            }
            return materials;
        }

        public List<Material> GetWorkerMaterials(int workerId)
        {
            bool newMaterial = true;
            Worker worker = db.Workers.Find(workerId);
            List<Material> materials = new List<Material>();
            foreach (var act in GetWorkerDeliveryActs(worker))
            {
                foreach (var materialDelivery in act.MaterialDeliveries)
                {
                    newMaterial = true;
                    foreach (var material in materials)
                    {
                        
                        if (material.Id == materialDelivery.Material.Id)
                        {
                            newMaterial = false;
                            break;
                        }
                        
                    }
                    if (newMaterial)
                    {
                        materials.Add(materialDelivery.Material);
                        newMaterial = false;
                    }
                }
            }
            return materials;
        }

        public List<DeliveryAct> GetWorkerDeliveryActs (Worker worker)
        {
            List<DeliveryAct> result = worker.DeliveryActs.ToList();
            return result;
            
        }

        public List<DeviceDelivery> GetDeviceDeliveries (Worker worker)
        {
            List<DeliveryAct> deliveryActs = db.DeliveryActs.Where(a => a.WorkerId == worker.Id).ToList();
            List<DeviceDelivery> deliveries = new List<DeviceDelivery>();
            foreach (var item in deliveryActs)
            {
                foreach (var deviceDelivery in item.DeviceDeliveries)
                {
                    Device device = deviceDelivery.Device;
                    DeviceDelivery lastDelivery = db.DeviceDeliveries.Where(d => d.DeviceId == device.Id).Last();
                    if (item.Id == lastDelivery.DeliveryAct.Id)
                    {
                        deliveries.Add(deviceDelivery);
                    }
                }
            }
            return deliveries;

        }

        public int GetReturnsCount (Worker worker)
        {
            int count = 0;
            List<DeliveryAct> deliveryActs = db.DeliveryActs.Where(a => a.WorkerId == worker.Id && a.DeliveryType.Description == "возврат на склад").ToList();
            foreach (var item in deliveryActs)
            {
                foreach (var deviceDelivery in item.DeviceDeliveries)
                    count++;
            }
            return count;
        }

        public void CheckWorkers() //Проверка задолженности по оборудованию
        {
            TimeSpan span;
            bool debt = false;
            foreach (var worker in db.Workers.Where(w => w.WorkerType.Description == "монтажник"))
            {
                var devices = GetWorkerDevices(worker);
                foreach (var item in devices)
                {
                    var lastDelivery = item.DeviceDeliveries.OrderByDescending(x => x.Id).First();
                    var act = lastDelivery.DeliveryAct;
                    span = DateTime.Now - lastDelivery.DeliveryAct.Date;
                    if (act.DeliveryType.Description == "выдача со склада" && span.Days > 11 && (item.CurrentState == "выдача со склада" || item.CurrentState == "включен в отчет"))
                    {
                        worker.DeliveryAvailible = false;
                        db.SaveChanges();
                        debt = true;
                        break;
                    }
                    if (debt) break;
                }

                if (!debt)
                {
                    worker.DeliveryAvailible = true;
                    db.SaveChanges();
                }
            }
                //foreach (var worker in db.Workers.Where(w => w.WorkerType.Description == "монтажник"))
                //{
                //    bool debt = false;
                //    foreach (var act in worker.DeliveryActs)
                //    {
                //        span = DateTime.Now - act.Date;
                //        if (span.Days > 10)
                //        {
                //            foreach (var deviceDelivery in act.DeviceDeliveries)
                //            {
                //                if (deviceDelivery.DeviceType.Type == "ПУ" && deviceDelivery.Device.CurrentState == "выдача со склада")
                //                {
                //                    worker.DeliveryAvailible = false;
                //                    db.SaveChanges();
                //                    debt = true;
                //                    break;
                //                }
                //            }
                //            if (debt) break;
                //        }
                //    }


                //    if (!debt)
                //    {
                //        worker.DeliveryAvailible = true;
                //        db.SaveChanges();
                //    }
                //}
            }
        }
    }

