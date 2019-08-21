using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DbManager
{
    /// <summary>Перечисление действий которые может совершить User над RegPoint или Substation</summary>
    public class ActionTypeName
    {
        private ActionTypeName(string value) { Name = value; }
        /// <summary>
        /// Название типа в виде строки (также и в БД)
        /// </summary>
        public string Name { get; set; }

        /// <summary>Пустая строка - ""</summary>
        public static ActionTypeName Undefined { get { return new ActionTypeName(""); } }
        /// <summary>"Создал"</summary>
        public static ActionTypeName Create { get { return new ActionTypeName("Создал"); } }
        /// <summary>"Изменил"</summary>
        public static ActionTypeName Edit { get { return new ActionTypeName("Изменил"); } }
        /// <summary>"Удалил"</summary>
        public static ActionTypeName Delete { get { return new ActionTypeName("Удалил"); } }
        /// <summary>"Добавил точку учета"</summary>
        public static ActionTypeName AddRegPoint { get { return new ActionTypeName("Добавил точку учета"); } }
        /// <summary>"Добавил точку учета"</summary>
        public static ActionTypeName RemoveRegPoint { get { return new ActionTypeName("Удалил точку учета"); } }
        /// <summary>"Добавил точку учета"</summary>
        public static ActionTypeName AddSubstation { get { return new ActionTypeName("Добавил подстанцию"); } }
        /// <summary>"Добавил точку учета"</summary>
        public static ActionTypeName RemoveSubstation { get { return new ActionTypeName("Удалил подстанцию"); } }
        /// <summary>"Переименовал подстанцию"</summary>
        public static ActionTypeName RenameSubstation { get { return new ActionTypeName("Переименовал подстанцию"); } }
        /// <summary>"Изменил данные о потребителе"</summary>
        public static ActionTypeName EditConsummer { get { return new ActionTypeName("Изменил данные о потребителе"); } }
        /// <summary>"Изменил данные акта допуска"</summary>
        public static ActionTypeName EditInstallAct { get { return new ActionTypeName("Изменил данные акта допуска"); } }
        /// <summary>"Установил флаг"</summary>
        public static ActionTypeName FlagSet { get { return new ActionTypeName("Установил флаг"); } }
        /// <summary>"Сбросил флаг"</summary>
        public static ActionTypeName FlagReset { get { return new ActionTypeName("Сбросил флаг"); } }
        /// <summary>"Создал письмо"</summary>
        public static ActionTypeName LetterAdd { get { return new ActionTypeName("Создал письмо"); } }
        /// <summary>"Удалил письмо"</summary>
        public static ActionTypeName LetterDelete { get { return new ActionTypeName("Удалил письмо"); } }
        /// <summary>"Распечатал письмо"</summary>
        public static ActionTypeName LetterPrint { get { return new ActionTypeName("Распечатал письмо"); } }
        /// <summary>"Изменил Track номер письма"</summary>
        public static ActionTypeName LetterTrackNumEdit { get { return new ActionTypeName("Изменил Track номер письма"); } }
        /// <summary>"Добавил комментарий"</summary>
        public static ActionTypeName AddComment { get { return new ActionTypeName("Добавил комментарий"); } }
        /// <summary>"Удалил комментарий"</summary>
        public static ActionTypeName DeleteComment { get { return new ActionTypeName("Удалил комментарий"); } }
        /// <summary>"Перенес точку учета"</summary>
        public static ActionTypeName ReplaceRegPoint { get { return new ActionTypeName("Перенес точку учета"); } }
        //
        //!!! ТУТ ДОБАВЛЯТЬ НОВЫЕ ДЕЙСТВИЯ !!!
        //

        /// <summary>Текстовое описание текущего типа действия</summary>
        public override string ToString()
        {
            return Name;
        }
    }
    /// <summary>Перечисление типов работника</summary>
    public class WorkerTypeName
    {
        private WorkerTypeName(string value) { Name = value; }
        /// <summary>
        /// Название типа в виде строки (также и в БД)
        /// </summary>
        public string Name { get; set; }

        /// <summary>"монтажник"</summary>
        public static WorkerTypeName Mounter { get { return new WorkerTypeName("монтажник"); } }
        /// <summary>"сборщик"</summary>
        public static WorkerTypeName Assembler { get { return new WorkerTypeName("сборщик"); } }
        /// <summary>"настройщик"</summary>
        public static WorkerTypeName Tuner { get { return new WorkerTypeName("настройщик"); } }
        /// <summary>"пнр"</summary>
        public static WorkerTypeName PNR { get { return new WorkerTypeName("пнр"); } }
        //
        //!!! ТУТ ДОБАВЛЯТЬ НОВЫЕ !!!
        //

        /// <summary>Текстовое описание текущего типа работника</summary>
        public override string ToString()
        {
            return Name;
        }
    }
    /// <summary>Перечисление статусов которые может иметь Device</summary>
    public class DeviceStateTypeName
    {
        private DeviceStateTypeName(string value) { Name = value; }
        /// <summary>
        /// Название типа в виде строки (также и в БД)
        /// </summary>
        public string Name { get; set; }

        /// <summary>Пустая строка - ""</summary>
        public static DeviceStateTypeName Undefined { get { return new DeviceStateTypeName(""); } }
        /// <summary>"прием на склад"</summary>
        public static DeviceStateTypeName Income { get { return new DeviceStateTypeName("прием на склад"); } }
        /// <summary>"выдача на сборку"</summary>
        public static DeviceStateTypeName ToAssembly { get { return new DeviceStateTypeName("выдача на сборку"); } }
        /// <summary>"выдача на настройку"</summary>
        public static DeviceStateTypeName ToTune { get { return new DeviceStateTypeName("выдача на настройку"); } }
        /// <summary>"выдача со склада"</summary>
        public static DeviceStateTypeName Outcome { get { return new DeviceStateTypeName("выдача со склада"); } }
        /// <summary>"брак"</summary>
        public static DeviceStateTypeName Defect { get { return new DeviceStateTypeName("брак"); } }
        /// <summary>"удаление из брака"</summary>
        public static DeviceStateTypeName DefectDelete { get { return new DeviceStateTypeName("удаление из брака"); } }
        /// <summary>"возврат с настройки"</summary>
        public static DeviceStateTypeName FromTune { get { return new DeviceStateTypeName("возврат с настройки"); } }
        /// <summary>"возврат со сборки"</summary>
        public static DeviceStateTypeName FromAssembly { get { return new DeviceStateTypeName("возврат со сборки"); } }
        /// <summary>"возврат на склад"</summary>
        public static DeviceStateTypeName ReturnToStore { get { return new DeviceStateTypeName("возврат на склад"); } }
        /// <summary>"возврат производителю"</summary>
        public static DeviceStateTypeName ReturnToMnfc { get { return new DeviceStateTypeName("возврат производителю"); } }
        /// <summary>"включен в отчет"</summary>
        public static DeviceStateTypeName AddToReport { get { return new DeviceStateTypeName("включен в отчет"); } }
        /// <summary>"удален из отчета"</summary>
        public static DeviceStateTypeName DeleteFromReport { get { return new DeviceStateTypeName("удален из отчета"); } }
        /// <summary>"принят куратором"</summary>
        public static DeviceStateTypeName AcceptedByCurator { get { return new DeviceStateTypeName("принят куратором"); } }
        /// <summary>"привязан к КДЕ"</summary>
        public static DeviceStateTypeName AddToKDE { get { return new DeviceStateTypeName("привязан к КДЕ"); } }
        /// <summary>"удален из КДЕ"</summary>
        public static DeviceStateTypeName DeleteFromKDE { get { return new DeviceStateTypeName("удален из КДЕ"); } }
        /// <summary>"привязан к ту"</summary>
        public static DeviceStateTypeName AddToTU { get { return new DeviceStateTypeName("привязан к ту"); } }
        /// <summary>"отвязан от ту"</summary>
        public static DeviceStateTypeName DeleteFromTU { get { return new DeviceStateTypeName("отвязан от ту"); } }

        /// <summary>Текстовое описание текущего статуса</summary>
        public override string ToString()
        {
            return Name;
        }
    }
    /// <summary> Перечисление типов состояния отчета </summary>
    public class ReportStateTypeName
    {
        private ReportStateTypeName(string value) { Name = value; }

        /// <summary> Название типа состояния отчета</summary>
        private string Name { get; set; }

       
        /// <summary>"в работе"</summary>
        public static ReportStateTypeName InWork { get { return new ReportStateTypeName("в работе"); } }
        /// <summary>"отправлен куратору"</summary>
        public static ReportStateTypeName SentToCurator { get { return new ReportStateTypeName("отправлен куратору"); } }
        /// <summary>"принят куратором"</summary>
        public static ReportStateTypeName AcceptedByCurator { get { return new ReportStateTypeName("принят куратором"); } }
        /// <summary>"с замечаниями куратора"</summary>
        public static ReportStateTypeName RemarksByCurator { get { return new ReportStateTypeName("с замечаниями куратора"); } }
        /// <summary>"импортирован"</summary>
        public static ReportStateTypeName Imported { get { return new ReportStateTypeName("импортирован"); } }
        /// <summary>"для импорта"</summary>
        public static ReportStateTypeName ForImport { get { return new ReportStateTypeName("для импорта"); } }

        /// <summary>Текстовое описание текущего типа состояния отчета</summary>
        public override string ToString()
        {
            return Name;
        }
    }
    /// <summary> Перечисление типов Link </summary>
    public class InstallPlaceTypeName
    {
        private InstallPlaceTypeName(string value) { Name = value; }

        /// <summary> Название типа</summary>
        private string Name { get; set; }

        /// <summary>Пустая строка - ""</summary>
        public static InstallPlaceTypeName Undefined { get { return new InstallPlaceTypeName(""); } }
        /// <summary>"РУ-0,4 кВ, руб-к №"</summary>
        public static InstallPlaceTypeName Rub { get { return new InstallPlaceTypeName("РУ-0,4 кВ, руб-к №"); } }
        /// <summary>"РУ-0,4 кВ, ввод №"</summary>
        public static InstallPlaceTypeName Vvod { get { return new InstallPlaceTypeName("РУ-0,4 кВ, ввод №"); } }
        /// <summary>"ВЛ-0,4 кВ, опора №"</summary>
        public static InstallPlaceTypeName Opora { get { return new InstallPlaceTypeName("ВЛ-0,4 кВ, опора №"); } }
        /// <summary>"ВЛ-0,4 кВ, фасад №"</summary>
        public static InstallPlaceTypeName Fasad { get { return new InstallPlaceTypeName("ВЛ-0,4 кВ, фасад №"); } }

        /// <summary>Текстовое описание текущего типа Link</summary>
        public override string ToString()
        {
            return Name;
        }
    }
    /// <summary> Перечисление состояний подстанции (для ПНР) </summary>
    public class SubstationStateName
    {
        private SubstationStateName(string value) { Name = value; }

        /// <summary> Название типа</summary>
        private string Name { get; set; }

        /// <summary>Пустая строка - ""</summary>
        public static SubstationStateName Undefined { get { return new SubstationStateName(""); } }

        /// <summary>Текстовое описание текущего типа</summary>
        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary> Перечисление типов Link (НЕТ В БАЗЕ, просто текст) </summary>
    public class LinkTypeName
    {
        private LinkTypeName(string value) { Name = value; }

        /// <summary> Название типа</summary>
        private string Name { get; set; }

        /// <summary>Пустая строка - ""</summary>
        public static LinkTypeName Undefined { get { return new LinkTypeName(""); } }
        /// <summary>"Встроенный GSM-модуль"</summary>
        public static LinkTypeName BuiltIn { get { return new LinkTypeName("Встроенный GSM-модуль"); } }
        /// <summary>"Внешний GSM модем"</summary>
        public static LinkTypeName External { get { return new LinkTypeName("Внешний GSM модем"); } }

        /// <summary>Текстовое описание текущего типа Link</summary>
        public override string ToString()
        {
            return Name;
        }
    }


    public static class EnumsHelper
    {
        // Поиск Id ActionType в базе используя перечисление
        public static int GetActionId(ActionTypeName actionTypeName)
        {
            ActionType actionType;
            using (StoreContext db = new StoreContext())
            {
                //Ищем тип действия по имени
                actionType = db.ActionTypes.FirstOrDefault(a => a.Name == actionTypeName.ToString());

                //Если нет такого типа в базе, то добавляем его
                if (actionType == null)
                {
                    actionType = new ActionType() { Name = actionTypeName.ToString() };
                    db.ActionTypes.Add(actionType);
                    db.SaveChanges();
                }
                //Возвращаем искомый Id
                return actionType.Id;
            }
        }

        // Поиск Id WorkerType в базе используя перечисление
        public static int GetWorkerTypeId(WorkerTypeName workerTypeName)
        {
            WorkerType workerType;
            using (StoreContext db = new StoreContext())
            {
                //Ищем тип работника по имени
                workerType = db.WorkerTypes.FirstOrDefault(a => a.Description == workerTypeName.ToString());

                //Если нет такого типа в базе, то добавляем его
                if (workerType == null)
                {
                    workerType = new WorkerType() { Description = workerTypeName.ToString() };
                    db.WorkerTypes.Add(workerType);
                    db.SaveChanges();
                }
                //Возвращаем искомый Id
                return workerType.Id;
            }
        }

        // Поиск Id StateDeviceType в базе
        public static int GetDeviceStateTypeId(DeviceStateTypeName typeName)
        {
            DeviceStateType stateType;
            using (StoreContext db = new StoreContext())
            {
                //Название статуса в виде строки
                string typeDescription = typeName.ToString();

                stateType = db.DeviceStateTypes.FirstOrDefault(t => t.Description == typeDescription);
                if (stateType == null)
                {
                    stateType = new DeviceStateType { Description = typeDescription };
                    db.DeviceStateTypes.Add(stateType);
                    db.SaveChanges();
                }

                return stateType.Id;
            }
        }

        // Поиск Id ReportState
        public static int GetReportStateId(ReportStateTypeName reportStateTypeName)
        {
            ReportState stateType;
            using (StoreContext db = new StoreContext())
            {
                //Ищем тип действия по имени
                stateType = db.ReportStates.FirstOrDefault(a => a.Description == reportStateTypeName.ToString());

                //Если нет такого типа в базе, то добавляем его
                if (stateType == null)
                {
                    stateType = new ReportState() { Description = reportStateTypeName.ToString() };
                    db.ReportStates.Add(stateType);
                    db.SaveChanges();
                }
                //Возвращаем искомый Id
                return stateType.Id;
            }
        }
        
        // Поиск Id InstallPlaceTypeId
        public static int GetInstallPlaceTypeId(InstallPlaceTypeName installPlaceTypeName)
        {
            InstallPlaceType installPlaceType;
            using (StoreContext db = new StoreContext())
            {
                //Ищем тип места установки
                installPlaceType = db.InstallPlaceTypes.FirstOrDefault(a => a.Name == installPlaceTypeName.ToString());

                //Если нет такого типа в базе, то добавляем его
                if (installPlaceType == null)
                {
                    installPlaceType = new InstallPlaceType() { Name = installPlaceTypeName.ToString() };
                    db.InstallPlaceTypes.Add(installPlaceType);
                    db.SaveChanges();
                }
                //Возвращаем искомый Id
                return installPlaceType.Id;
            }
        }

        // Поиск Id SubstationState
        public static int GetSubstationStateId(SubstationStateName substationStateName)
        {
            SubstationState substationState;
            using (StoreContext db = new StoreContext())
            {
                //Ищем тип места установки
                substationState = db.SubstationStates.FirstOrDefault(a => a.Name == substationStateName.ToString());

                //Если нет такого типа в базе, то добавляем его
                if (substationState == null)
                {
                    substationState = new SubstationState() { Name = substationStateName.ToString() };
                    db.SubstationStates.Add(substationState);
                    db.SaveChanges();
                }
                //Возвращаем искомый Id
                return substationState.Id;
            }
        }

        public static void SyncAllEnums()
        {
            //ActionTypeName
            foreach (PropertyInfo pi in typeof(ActionTypeName).GetProperties(BindingFlags.Public | BindingFlags.Static))
                GetActionId((ActionTypeName)pi.GetValue(null));
            //WorkerTypeName
            foreach (PropertyInfo pi in typeof(WorkerTypeName).GetProperties(BindingFlags.Public | BindingFlags.Static))
                GetWorkerTypeId((WorkerTypeName)pi.GetValue(null));
            //DeviceStateTypeName
            foreach (PropertyInfo pi in typeof(DeviceStateTypeName).GetProperties(BindingFlags.Public | BindingFlags.Static))
                GetDeviceStateTypeId((DeviceStateTypeName)pi.GetValue(null));
            //ReportStateTypeName
            foreach (PropertyInfo pi in typeof(ReportStateTypeName).GetProperties(BindingFlags.Public | BindingFlags.Static))
                GetReportStateId((ReportStateTypeName)pi.GetValue(null));
            //InstallPlaceTypeName
            foreach (PropertyInfo pi in typeof(InstallPlaceTypeName).GetProperties(BindingFlags.Public | BindingFlags.Static))
                GetInstallPlaceTypeId((InstallPlaceTypeName)pi.GetValue(null));
            //SubstationStateName
            foreach (PropertyInfo pi in typeof(SubstationStateName).GetProperties(BindingFlags.Public | BindingFlags.Static))
                GetSubstationStateId((SubstationStateName)pi.GetValue(null));
        }
    }
}
