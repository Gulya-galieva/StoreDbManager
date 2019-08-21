using DbManager;
using StoreDbManager.RegPoint;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreDbManager.Worker
{
    public class PaymentReportsManager
    {
        StoreContext db;
        public PaymentReportsManager() { db = new StoreContext(); }
        public PaymentReportsManager(StoreContext context) { db = context; }

        /// <summary> Список точек учета связанных с монтажником, возвращает список RegPointInfo, в которых содержится вся основная информация о точках учета </summary>
        /// <param name="workerId">Id работника</param>
        /// <returns></returns>
        public List<RegPointInfo> GetWorkerRegPointsInfo(int workerId)
        {
            return AllWorkerPointsQuery(workerId).ToList();
        }

        /// <summary> Список точек учета связанных с монтажником, не прикрепленный к отчету на оплату </summary>
        /// <param name="workerId">Id работника</param>
        /// <returns></returns>
        public List<RegPointInfo> GetAvailableWorkerRegPointsInfo(int workerId)
        {
            return AllWorkerPointsQuery(workerId).Where(p=> !p.IsInPayReportAlready).ToList();
        }

        /// <summary>
        /// Получает список отчетов на оплату, привязанных к работнику
        /// </summary>
        public List<PaymentReport> GetWorkerPaymentReports(int workerId)
        {
            var reports = from pr in db.PaymentReports
                          where pr.WorkerId == workerId
                          select pr;
            return reports.ToList();
        }
        /// <summary>
        /// Получает список отчетов на оплату, привязанных к работнику
        /// </summary>
        public List<RegPointInfo> GetPointsInReport(int paymentReportId)
        {
            var worker = db.Workers.FirstOrDefault(w => w.PaymentReports.Any(pr => pr.Id == paymentReportId));
            if (worker != null)
                return AllWorkerPointsQuery(worker.Id).Where(p => p.PaymentReportId == paymentReportId).ToList();
            else
                return new List<RegPointInfo>();
        }

        //Запрос на все точки учета связанные с монтажником
        private IQueryable<RegPointInfo> AllWorkerPointsQuery(int workerId)
        {
            //По типу работников
            int workerTypeId = db.WorkerTypes.FirstOrDefault(wt => wt.Description == WorkerTypeName.Mounter.ToString()).Id;
            //Только выданые счетчики
            int dTypeId = db.DeliveryTypes.FirstOrDefault(t => t.Description == "выдача со склада").Id;
            //Событие создания ТУ
            int rpActionCreateId = EnumsHelper.GetActionId(ActionTypeName.Create);

            return (from rp in db.RegPoints
                    join rpFlags in db.RegPointFlags on rp.Id equals rpFlags.RegPointId
                    join rpAction in db.RegPointActions on rp.Id equals rpAction.RegPointId
                    join device in db.Devices on rp.DeviceId equals device.Id
                    join deviceDelivery in db.DeviceDeliveries on device.Id equals deviceDelivery.DeviceId
                    join deliveryAct in db.DeliveryActs on deviceDelivery.DeliveryActId equals deliveryAct.Id
                    join worker in db.Workers on deliveryAct.WorkerId equals worker.Id
                    join payReportRow in db.PaymentReportRegPoints on rp.Id equals payReportRow.RegPointId into pRows
                    from payReportRow in pRows.DefaultIfEmpty()
                    where worker.WorkerTypeId == workerTypeId         //Фильтр по типу работника ("Монтажник")
                    where rpAction.ActionTypeId == rpActionCreateId   //Фильтр по типу события ТУ ("Создал")
                    where deliveryAct.DeliveryTypeId == dTypeId       //"выдача со склада"              
                    orderby deliveryAct.Date descending
                    select new RegPointInfo()
                    {
                        //Pay
                        IsInPayReportAlready = (payReportRow != null) ? true : false,
                        PaymentReportId = (payReportRow != null) ? payReportRow.PaymentReportId : 0,
                        CostRUB = (payReportRow != null) ? payReportRow.CostRUB : 0,
                        WorkType = (payReportRow != null) ? payReportRow.WorkType : PaymentReportWorkType.None,
                        //RegPoint
                        RegPointId = rp.Id,
                        RegPointStatus = rp.Status,
                        DateAdd = rpAction.Date,
                        RegionName = rp.Substation.NetRegion.Name,
                        SubstationName = rp.Substation.Name,
                        OAddress = rp.Consumer.FormatOAddress(),
                        //Worker
                        WorkerId = worker.Id,
                        //Device
                        DeviceId = device.Id,
                        Serial = device.SerialNumber,
                        DeviceDescription = device.DeviceType.Description,
                        DeviceModel = device.DeviceType.Name,
                        IsLinkOk = rpFlags.IsLinkOk
                    })
                    .GroupBy(rp => rp.RegPointId)  //Группируем по id
                    .Select(g => g.First())
                    .Where(p => p.WorkerId == workerId); //Id работника
        }
    }
}
