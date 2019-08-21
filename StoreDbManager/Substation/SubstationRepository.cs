using DbManager;
using StoreDbManager.RegPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreDbManager.Substation
{
    public class SubstationRepository
    {
        StoreContext db;
        public SubstationRepository(StoreContext context)
        {
            db = context;
        }

        /// <summary>
        /// Список подстанции в том же районе что и эта подстанция (substationId)
        /// </summary>
        /// <param name="substationId">Id подстанции</param>
        /// <returns></returns>
        public List<DbManager.Substation> NearSubstations(int substationId)
        {
            int regionId = db.Substations.Find(substationId).NetRegionId;
            return db.Substations.Where(s => s.NetRegionId == regionId).ToList();
        }

        /// <summary>
        /// Список точек учета по подстанции
        /// </summary>
        /// <param name="id">Id подстанции</param>
        /// <returns></returns>
        public List<RegPointInfo> RegPoints(int id)
        {
            return AllPointsInfoQuery().Where(p => p.RegPointStatus == RegPointStatus.Default && p.SubstationId == id).ToList();
        }

        //Запрос на все точки учета связанные с монтажником
        private IQueryable<RegPointInfo> AllPointsInfoQuery()
        {
            //По типу работников
            //int workerTypeId = db.WorkerTypes.FirstOrDefault(wt => wt.Description == WorkerTypeName.Mounter.ToString()).Id;
            //Только выданые счетчики
            //int dTypeId = db.DeliveryTypes.FirstOrDefault(t => t.Description == "выдача со склада").Id;
            //Событие создания ТУ
            //int rpActionCreateId = EnumsHelper.GetActionId(ActionTypeName.Create);

            return (from rp in db.RegPoints
                    join installPlaceType in db.InstallPlaceTypes on rp.InstallAct.InstallPlaceTypeId equals installPlaceType.Id
                    join rpFlags in db.RegPointFlags on rp.Id equals rpFlags.RegPointId
                    join rpAction in db.RegPointActions on rp.Id equals rpAction.RegPointId
                    join device in db.Devices on rp.DeviceId equals device.Id
                    join deviceDelivery in db.DeviceDeliveries on device.Id equals deviceDelivery.DeviceId
                    join deliveryAct in db.DeliveryActs on deviceDelivery.DeliveryActId equals deliveryAct.Id
                    join worker in db.Workers on deliveryAct.WorkerId equals worker.Id
                    join payReportRow in db.PaymentReportRegPoints on rp.Id equals payReportRow.RegPointId into pRows
                    from payReportRow in pRows.DefaultIfEmpty()
                    join linkD in db.Links on device.Id equals linkD.DeviceId into lDs
                    from linkD in lDs.DefaultIfEmpty()   //Link привязанные к устройствам
                    join linkS in db.SubstationLinks on rp.SubstationId equals linkS.SubstationId into lSs
                    from linkS in lSs.DefaultIfEmpty()   //Link привязанные к подстанции
                    //where worker.WorkerTypeId == workerTypeId         //Фильтр по типу работника ("Монтажник")
                    //where rpAction.ActionTypeId == rpActionCreateId   //Фильтр по типу события ТУ ("Создал")
                    //where deliveryAct.DeliveryTypeId == dTypeId       //"выдача со склада"              
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
                        InstallPlace = installPlaceType.Name + rp.InstallAct.InstallPlaceNumber,
                        DateAdd = rpAction.Date,
                        RegionName = rp.Substation.NetRegion.Name,
                        SubstationName = rp.Substation.Name,
                        SubstationId = rp.SubstationId,
                        OAddress = rp.Consumer.FormatOAddress(),
                        UAddress = rp.Consumer.FormatUAddress(),
                        //Worker
                        WorkerId = worker.Id,
                        WorkerFName = worker.Name,
                        WorkerLName = worker.Surname,
                        WorkerMName = worker.MIddlename,
                        //Device
                        DeviceId = device.Id,
                        Serial = device.SerialNumber,
                        PhoneNumber = (device.DeviceType.Description.ToLower().Contains("plc")) ? linkS.PhoneNumber : linkD.PhoneNumber,
                        DeviceDescription = device.DeviceType.Description,
                        DeviceModel = device.DeviceType.Name,
                        IsLinkOk = rpFlags.IsLinkOk,
                        IsAscueChecked = rpFlags.IsAscueChecked,
                        IsAscueOk = rpFlags.IsAscueOk,
                        IsReplace = rpFlags.IsReplace
                    })
                    .GroupBy(rp => rp.RegPointId)  //Группируем по id
                    .Select(g => g.First());
        }
    }
}
