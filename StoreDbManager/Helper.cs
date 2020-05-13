using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reflection;

namespace DbManager
{
    //
    // bool Worker.IsWorkerType()
    // void Worker.SetWorkerTypeId()
    //
    // bool Device.IsCurrentState()
    // bool Device.IsEnableToAddTU()
    // void Device.AddState()
    //
    // void RegPoint.AddAction()
    // string RegPoint.Remove()
    //
    // void Substation.AddAction()
    // string Substation.AddRegPoint()
    // string Substation.Remove()
    //
    // void NetRegion.AddAction()
    // string NetRegion.AddSubstation()
    //
    /// <summary>
    /// Класс для методов расширений
    /// </summary>
    public static class Helper
    {
        #region Расширения Worker
        /// <summary>
        /// Проверка типа (или должности) работника 
        /// </summary>
        /// <returns>Возвращает true если тип совпадает с workerTypeName</returns>
        public static bool IsWorkerType(this Worker worker, WorkerTypeName workerTypeName)
        {
            return worker.WorkerType.Description == workerTypeName.ToString();
        }
        /// <summary>
        /// Задать работнику его тип (или должность) из WorkerTypeName.[ТипНаВыбор]
        /// Устанавливает только внешний ключ WorkerTypeId.
        /// </summary>
        public static void SetWorkerTypeId(this Worker worker, WorkerTypeName workerTypeName)
        {
            worker.WorkerTypeId = EnumsHelper.GetWorkerTypeId(workerTypeName);
        }

        /// <summary> Получить полное имя работника в формате "[Фамилия] [Имя] [Отчество]" </summary>
        /// <param name="worker"></param>
        /// <returns></returns>
        public static string GetFullName(this Worker worker)
        {
            return worker.Surname + " " + worker.Name + " " + worker.MIddlename;
        }
        #endregion

        #region Расширения Device
        /// <summary>
        /// Проверка текущего состояния счетчика
        /// </summary>
        /// <returns>Возвращает true если состояние совпадает с deviceStateTypeName</returns>
        public static bool IsCurrentState(this Device device, DeviceStateTypeName deviceStateTypeName)
        {
            return device.CurrentState == deviceStateTypeName.ToString();
        }
        /// <summary>
        /// Проверяет можно ли устройство (ПУ) прикрепить к ТУ (точке учета)
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static bool IsEnableToAddTU(this Device device)
        {
            return (device.CurrentState == DeviceStateTypeName.Outcome.ToString() ||
                    device.CurrentState == DeviceStateTypeName.AddToReport.ToString());
        }
        /// <summary>
        /// Проверяет можно ли устройство (ПУ) импортировать (точке учета)
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static bool IsEnableToImportTU(this Device device)
        {
            return device.RegPoints.FirstOrDefault(r => r.Status != RegPointStatus.Demounted) == null;
        }
        /// <summary>
        /// Добавить действие над Оборудованием
        /// </summary>
        /// <param name="device"></param>
        /// <param name="typeName">Тип действия из списка</param>
        /// <param name="userId">Id пользователя который совершает действие</param>
        /// <param name="comment">Если не нужен текстовый комментарий, то подствьте null</param>
        public static void AddState(this Device device, DeviceStateTypeName typeName, int userId, string comment)
        {
            //Новый статус в историю
            device.DeviceStates.Add(new DeviceState() { DeviceStateTypeId = EnumsHelper.GetDeviceStateTypeId(typeName), UserId = userId, Comment = comment, Date = DateTime.Now });

            string typeNameString = typeName.ToString();
            //Новый текущий статус (!) Важный момент
            //Этапы, имя которых совпадает должно совпадать с текущим статусом
            if (typeNameString == DeviceStateTypeName.Income.ToString() ||
               typeNameString == DeviceStateTypeName.ToTune.ToString() ||
               typeNameString == DeviceStateTypeName.ToAssembly.ToString() ||
               typeNameString == DeviceStateTypeName.Outcome.ToString() ||
               typeNameString == DeviceStateTypeName.ReturnToMnfc.ToString() ||
               typeNameString == DeviceStateTypeName.Defect.ToString() ||
               typeNameString == DeviceStateTypeName.FromAssembly.ToString() ||
               typeNameString == DeviceStateTypeName.FromTune.ToString() ||
               typeNameString == DeviceStateTypeName.AddToReport.ToString() ||
               typeNameString == DeviceStateTypeName.AcceptedByCurator.ToString() ||
               typeNameString == DeviceStateTypeName.AddToTU.ToString())
                device.CurrentState = typeName.ToString();

            //Этапы после которых текущий статус равен "выдача со склада"
            if (typeNameString == DeviceStateTypeName.DeleteFromReport.ToString() ||
                typeNameString == DeviceStateTypeName.DeleteFromTU.ToString())
                device.CurrentState = DeviceStateTypeName.Outcome.ToString();

            //Этапы после которых текущий статус равен "прием на склад"
            if (typeNameString == DeviceStateTypeName.ReturnToStore.ToString() ||
                typeNameString == DeviceStateTypeName.DefectDelete.ToString())
                device.CurrentState = DeviceStateTypeName.Income.ToString();
        }
        #endregion

        #region Расширения RegPoint
        /// <summary>
        /// Добавить действие над Точкой учета
        /// </summary>
        /// <param name="regPoint"></param>
        /// <param name="actionTypeName">Тип действия из списка</param>
        /// <param name="userId">Id пользователя который совершает действие</param>
        /// <param name="comment">Если не нужен текстовый комментарий, то подствьте null</param>
        public static void AddAction(this RegPoint regPoint, ActionTypeName actionTypeName, int userId, string comment)
        {
            //Добавляем в подстанцию действия
            regPoint.RegPointActions.Add(new RegPointAction() { ActionTypeId = EnumsHelper.GetActionId(actionTypeName), UserId = userId, Comment = comment, Date = DateTime.Now });
        }
        /// <summary> Удаляет точку учета и всю связанную с ней информацию (если она добавлена вручную) </summary>
        /// <param name="rp"></param>
        /// <param name="userId">Id пользователя который удаляет точку учета</param>
        /// <returns></returns>
        public static string Remove(this RegPoint rp, int userId)
        {
            if (rp == null) return "Ошибка: Не удалось найти ТУ";

            using (StoreContext db = new StoreContext())
            {
                //Для того чтобы не затрагивать внешний контекст ищем объект в БД еще раз
                var regPoint = db.RegPoints.Find(rp.Id);
                if (regPoint == null) return "Ошибка: Не удалось найти ТУ";
                //Выставляем статус у прикрепленного устройства - "отвязано от ТУ"
                if (regPoint.Device != null)
                    regPoint.Device.AddState(DeviceStateTypeName.DeleteFromTU, userId,
                        regPoint.Substation.Name + " " + regPoint.InstallAct.InstallPlaceType.Name + regPoint.InstallAct.InstallPlaceNumber + " " + regPoint.Consumer.O_Street + " " + regPoint.Consumer.O_House);
                //Событие удаления в историю
                regPoint.Substation.AddAction(ActionTypeName.RemoveRegPoint, userId,
                    regPoint.Substation.Name + " [" + regPoint.Device?.SerialNumber + "] " + regPoint.InstallAct.InstallPlaceType.Name + regPoint.InstallAct.InstallPlaceNumber + " " + regPoint.Consumer.O_Street + " " + regPoint.Consumer.O_House);
                //Вся инфа по ТУ также удалится
                if(regPoint.RegPointFlags.ReportedByMounter) return "Запрет на удаление. Точка импортирована из электронного отчета";
                db.RegPoints.Remove(regPoint);
                db.SaveChanges();
                rp = null;
                return "Точка учета успешно удалена из базы";
            }
        }
        /// <summary> Удаляет точку учета и всю связанную с ней информацию (даже если импортирована из отчета) </summary>
        /// <param name="rp"></param>
        /// <param name="userId">Id пользователя который удаляет точку учета</param>
        /// <returns></returns>
        public static string RemoveForce(this RegPoint rp, int userId)
        {
            if (rp == null) return "Ошибка: Не удалось найти ТУ";

            using (StoreContext db = new StoreContext())
            {
                //Для того чтобы не затрагивать внешний контекст ищем объект в БД еще раз
                var regPoint = db.RegPoints.Find(rp.Id);
                if (regPoint == null) return "Ошибка: Не удалось найти ТУ";
                //Выставляем статус у прикрепленного устройства - "отвязано от ТУ"
                if (regPoint.Device != null)
                    regPoint.Device.AddState(DeviceStateTypeName.DeleteFromTU, userId,
                        regPoint.Substation.Name + " " + regPoint.InstallAct.InstallPlaceType.Name + regPoint.InstallAct.InstallPlaceNumber + " " + regPoint.Consumer.O_Street + " " + regPoint.Consumer.O_House);
                //Событие удаления в историю
                regPoint.Substation.AddAction(ActionTypeName.RemoveRegPoint, userId,
                    regPoint.Substation.Name + " [" + regPoint.Device?.SerialNumber + "] " + regPoint.InstallAct.InstallPlaceType.Name + regPoint.InstallAct.InstallPlaceNumber + " " + regPoint.Consumer.O_Street + " " + regPoint.Consumer.O_House);
                //Вся инфа по ТУ также удалится
                db.RegPoints.Remove(regPoint);
                db.SaveChanges();
                rp = null;
                return "Точка учета успешно удалена из базы";
            }
        }
        #endregion

        #region Расширения Substation
        /// <summary>
        /// Добавить действие над Подстанцией
        /// </summary>
        /// <param name="substation"></param>
        /// <param name="actionTypeName">Тип действия из списка</param>
        /// <param name="userId">Id пользователя который совершает действие</param>
        /// <param name="comment">Если не нужен текстовый комментарий, то подствьте null</param>
        public static void AddAction(this Substation substation, ActionTypeName actionTypeName, int userId, string comment)
        {
            //Добавляем в подстанцию действия
            substation.SubstationActions.Add(new SubstationAction() { ActionTypeId = EnumsHelper.GetActionId(actionTypeName), UserId = userId, Comment = comment, Date = DateTime.Now });
        }
        /// <summary>
        /// Добавляет новую точку учета и создает все связанные записи в БД. Так же добавляет действие Create для этой точки.
        /// </summary>
        /// <param name="substation"></param>
        /// <param name="deviceId">Id привязанного к точке прибора учета</param>
        /// <param name="userId">Id пользователя, который создает эту точку учета</param>
        public static string AddRegPoint(this Substation substation, int deviceId, int userId)
        {
            return substation.AddRegPoint(deviceId, userId, null, null, null);
        }
        /// <summary>
        /// Добавляет новую точку учета и создает все связанные записи в БД. Так же добавляет действие Create для этой точки.
        /// </summary>
        /// <param name="substation"></param>
        /// <param name="deviceId">Id привязанного к точке прибора учета</param>
        /// <param name="userId">Id пользователя, который создает эту точку учета</param>
        /// <param name="regPointFlagsData">Объект с флагами. Если нет первоначальных данных, то отправляй null</param>
        /// <param name="installActData">Инфа для акта. Если нет первоначальных данных, то отправляй null</param>
        /// <param name="consumerData">Инфа о потребителе. Если нет первоначальных данных, то отправляй null</param>
        public static string AddRegPoint(this Substation substation, int deviceId, int userId, RegPointFlags regPointFlagsData, InstallAct installActData, Consumer consumerData)
        {
            //Связанные таблицы InstallAct, RegPointFlags, Consumer создаются в конструкторе RegPoint
            //Создаем записи в связанных таблицах
            RegPoint regPoint = new RegPoint();

            //Обязательно метку о том кто создал и когда
            regPoint.RegPointActions.Add(new RegPointAction() { ActionTypeId = EnumsHelper.GetActionId(ActionTypeName.Create), UserId = userId, Date = DateTime.Now });

            //Проверим статус устройства и привяжем его к точке учета
            using (StoreContext db = new StoreContext())
            {
                var device = db.Devices.FirstOrDefault(d => d.Id == deviceId);
                if (device == null) return "Этого прибора учета нет в базе";
                device.AddState(DeviceStateTypeName.AddToTU, userId, null);
                regPoint.InstallAct = installActData ?? new InstallAct() { InstallPlaceTypeId = EnumsHelper.GetInstallPlaceTypeId(InstallPlaceTypeName.Undefined) };
                regPoint.RegPointFlags = regPointFlagsData ?? new RegPointFlags();
                regPoint.Consumer = consumerData ?? new Consumer();
                regPoint.DeviceId = device.Id;
                substation.RegPoints.Add(regPoint);
                substation.AddAction(ActionTypeName.AddRegPoint, userId, null);
                if (device.DeviceType.Type.ToLower() == "успд")
                    regPoint.Status = RegPointStatus.USPD;
                db.SaveChanges();
                return "Точка учета создана";
            }
        }
        /// <summary>
        /// Удаляет подстанцию и всю связанную с ней информацию (действия и точки учета)
        /// </summary>
        /// <param name="sub"></param>
        /// <param name="userId">Id пользователя который удаляет подстанцию</param>
        /// <returns></returns>
        public static string Remove(this Substation sub, int userId)
        {
            if (sub == null) return "Ошибка: Не удалось найти подстанцию";

            using (StoreContext db = new StoreContext())
            {
                //Для того чтобы не затрагивать внешний контекст ищем объект в БД еще раз
                var substation = db.Substations.Find(sub.Id);
                if (substation == null) return "Ошибка: Не удалось найти подстанцию";
                //Выставляем статусы у всех прикрепленных устройств - "отвязано от ТУ"
                foreach (var point in substation.RegPoints)
                {
                    if (point.Device != null)
                        point.Device.AddState(DeviceStateTypeName.DeleteFromTU, userId,
                            point.Substation.Name + " " + point.InstallAct.InstallPlaceType.Name + point.InstallAct.InstallPlaceNumber + " " + point.Consumer.O_Street + " " + point.Consumer.O_House);
                }
                //Все привязанные к подстанции точки учета удалятся тоже (и вся инфа по этим ТУ также удалится)
                substation.NetRegion.AddAction(ActionTypeName.RemoveSubstation, userId, substation.Name);
                db.Substations.Remove(substation);
                db.SaveChanges();
                sub = null;
                return "Подстанция успешно удалена из базы";
            }
        }
        #endregion

        #region Расширения NetRegion
        /// <summary>
        /// Добавляет действие на Регионом (районом)
        /// </summary>
        /// <param name="netRegion"></param>
        /// <param name="actionTypeName">Тип действия из списка</param>
        /// <param name="userId">Id пользователя который совершает действие</param>
        /// <param name="comment">Если не нужен текстовый комментарий, то подствьте null</param>
        public static void AddAction(this NetRegion netRegion, ActionTypeName actionTypeName, int userId, string comment)
        {
            //Добавляем в район действия
            netRegion.NetRegionActions.Add(new NetRegionAction() { ActionTypeId = EnumsHelper.GetActionId(actionTypeName), UserId = userId, Comment = comment, Date = DateTime.Now });
        }
        /// <summary>
        /// Проверяет есть ли подстанция с таким же именем, и если нет, то добавляет и создает действие Create для подстанции. Данные добавляются только в контекст, db.SaveChanges() нужно вызывать вручную.
        /// </summary>
        /// <param name="netRegion"></param>
        /// <param name="substationName">Имя новой подстанции</param>
        /// <param name="userId">Id пользователя, который выполняет это действие</param>
        /// <returns>Возвращает сообщение (string) о результате выполнения</returns>
        public static string AddSubstation(this NetRegion netRegion, string substationName, int userId)
        {
            if (netRegion == null) return "Ошибка: Не удалось загрузить Район";

            Substation newSubstation = new Substation();
            newSubstation.Name = substationName;
            newSubstation.SubstationStateId = 1;
            //Обязательно метку о том кто создал и когда
            newSubstation.AddAction(ActionTypeName.Create, userId, null);

            using (StoreContext db = new StoreContext())
            {
                //Проверим есть ли подстанция с таким названием
                var sub = db.Substations.FirstOrDefault(s => s.Name == substationName);
                if (sub != null) return substationName + " уже есть в этом районе";
                netRegion.Substations.Add(newSubstation);
                netRegion.AddAction(ActionTypeName.AddSubstation, userId, substationName);
                return substationName + " успешно добавлена в " + netRegion.Name;
            }
        }
        #endregion

        #region Расширения Reports
        /// <summary>
        /// Изменить статус отчета ВЛ
        /// </summary>
        /// <param name="report">Изменяемый отчет</param>
        /// <param name="stateTypeName">Новый тип состояния отчета</param>
        public static void ChangeState(this MounterReportUgesAL report, ReportStateTypeName stateTypeName)
        {
            //Изменение типа состояния отчета
            report.ReportStateId = EnumsHelper.GetReportStateId(stateTypeName);
        }

        /// <summary>
        /// Изменить статус отчета ТП/РП
        /// </summary>
        /// <param name="report">Изменяемый отчет</param>
        /// <param name="stateTypeName">Новый тип состояния отчета</param>
        public static void ChangeState(this SBReport report, ReportStateTypeName stateTypeName)
        {
            //Изменение типа состояния отчета
            report.ReportStateId = EnumsHelper.GetReportStateId(stateTypeName);
        }

        /// <summary>
        /// Изменить статус отчета УСПД
        /// </summary>
        /// <param name="report">Изменяемый отчет</param>
        /// <param name="stateTypeName">Новый тип состояния отчета</param>
        public static void ChangeState(this USPDReport report, ReportStateTypeName stateTypeName)
        {
            //Изменение типа состояния отчета
            report.ReportStateId = EnumsHelper.GetReportStateId(stateTypeName);
        }

        /// <summary>
        /// Изменить статус отчета Демонтажа
        /// </summary>
        /// <param name="report">Изменяемый отчет</param>
        /// <param name="stateTypeName">Новый тип состояния отчета</param>
        public static void ChangeState(this UnmountReport report, ReportStateTypeName stateTypeName)
        {
            //Изменение типа состояния отчета
            report.ReportStateId = EnumsHelper.GetReportStateId(stateTypeName);
        }
        #endregion

        #region Расширения Consumer
        public static string FormatAddress(this Consumer consumer, string Local, string Local_Secondary, string Street, string House, string Build, string Flat)
        {
            string result = "";
            if (Local != "" && Local != null) result += Local;
            if (Local_Secondary != "" && Local_Secondary != null) result += ", " + Local_Secondary;
            if (Street != "" && Street != null) result += ", ул. " + Street;
            if (House != "" && House != "-" && House != "0" && House != null) result += ", д. " + House;
            if (Build != "" && Build != "-" && Build != "0" && Build != null) result += "/" + Build;
            if (Flat != "" && Flat != "-" && Flat != "0" && Flat != null) result += ", кв. " + Flat;

            return result;
        }

        /// <summary>
        /// Получить отформатированный Адрес объекта потребителя
        /// </summary>
        /// <param name="consumer"></param>
        /// <returns></returns>
        public static string FormatOAddress(this Consumer consumer)
        {
            return consumer.FormatAddress(consumer.O_Local, consumer.O_Local_Secondary, consumer.O_Street, consumer.O_House, consumer.O_Build, consumer.O_Flat);
        }

        /// <summary>
        /// Получить отформатированный юридический Адрес потребителя
        /// </summary>
        /// <param name="consumer"></param>
        /// <returns></returns>
        public static string FormatUAddress(this Consumer consumer)
        {
            return consumer.FormatAddress(consumer.U_Local, consumer.U_Local_Secondary, consumer.U_Street, consumer.U_House, consumer.U_Build, consumer.U_Flat);
        }
        #endregion

        #region Расширения PaymentReport
        /// <summary>
        /// Приводит период отчета в формат "с [начало периода] по [конец периода]"
        /// </summary>
        /// <param name="report"></param>
        /// <returns></returns>
        public static string GetPeriodString(this PaymentReport report)
        {
            var datePeriodEnd = report.DatePeriodStart.Day == 1 ?
                   new DateTime(report.DatePeriodStart.Year, report.DatePeriodStart.Month, 15) :
                   new DateTime(report.DatePeriodStart.Year, report.DatePeriodStart.Month, 1).AddDays(-1);
                   /*new DateTime(report.DatePeriodStart.Year, report.DatePeriodStart.Month + 1, 1).AddDays(-1);*/
            return "c " + report.DatePeriodStart.ToString("dd MMMM yyyy") + " по " + datePeriodEnd.ToString("dd MMMM yyyy");
        }
        #endregion
    }
}
