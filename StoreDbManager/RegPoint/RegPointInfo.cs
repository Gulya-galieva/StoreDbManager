using DbManager;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreDbManager.RegPoint
{
    /// <summary>
    /// Инфо по точке учета для отчетов на оплату подрядчикам
    /// </summary>
    public class RegPointInfo
    {
        /// <summary> Id точки учета (номер акта) </summary>
        public int RegPointId { get; set; }
        /// <summary> Тип устройства (ПУ, УСПД, plc модем) </summary>
        public RegPointStatus RegPointStatus { get; set; }
        /// <summary> Имя района в котором установлена точка учета </summary>
        public string RegionName { get; set; }
        /// <summary> Место установки точки учета </summary>
        public string InstallPlace { get; set; }
        /// <summary> Id подстанции в которой установлена точка учета </summary>
        public int SubstationId { get; set; }
        /// <summary> Название подстанции в которой установлена точка учета </summary>
        public string SubstationName { get; set; }
        /// <summary> Адрес по которому установлена точка учета </summary>
        public string OAddress { get; set; }
        /// <summary> Юридический адрес потребителя </summary>
        public string UAddress { get; set; }
        /// <summary> Id устройства привязанного к точке учета </summary>
        public int DeviceId { get; set; }
        /// <summary> Id работника к кому привязано устройство </summary>
        public int WorkerId { get; set; }
        /// <summary> Имя работника к кому привязано устройство </summary>
        public string WorkerFName { get; set; }
        /// <summary> Фамилия работника к кому привязано устройство </summary>
        public string WorkerLName { get; set; }
        /// <summary> Отчество работника к кому привязано устройство </summary>
        public string WorkerMName { get; set; }
        /// <summary> Описание типа устройства (1ф 3ф и тд) </summary>
        public string DeviceDescription { get; set; }
        /// <summary> Модель устройства полное наименование </summary>
        public string DeviceModel { get; set; }
        /// <summary> Серийный номер устройства </summary>
        public string Serial { get; set; }
        /// <summary> Номер сим карты в модеме (встроенном либо на подстанции) </summary>
        public string PhoneNumber { get; set; }
        /// <summary> Проверена ли связь с устройством </summary>
        public bool IsLinkOk { get; set; }
        /// <summary>ПУ повторно проверен и будет заведён в ПО АСКУЭ</summary>
        public bool IsAscueChecked { get; set; }
        /// <summary>ПУ работает в ПО АСКУЭ</summary>
        public bool IsAscueOk { get; set; }
        /// <summary>Замена ПУ</summary>
        public bool IsReplace { get; set; }
        /// <summary> Дата создания точки учета в БД </summary>
        public DateTime DateAdd { get; set; }
        /// <summary> Прикреплена ли точка учета к отчету на оплату </summary>
        public bool IsInPayReportAlready { get; set; }
        /// <summary> Id отчета на оплату к которому привязана точка учета (если нет, то 0) </summary>
        public int PaymentReportId { get; set; }
        /// <summary> Id элемента в отчете </summary>
        public int PaymentReportRegPointId { get; set; }
        /// <summary> Цена за точку учета, если точна не в отчете то 0 руб. </summary>
        public double CostRUB { get; set; }
        /// <summary> Тип работ при оплате: монтажб демонтаж или ПНР </summary>
        public PaymentReportWorkType WorkType { get; set; }

        public string PeriodDay { get => DatePeriodStart.Day.ToString(); }
        public string PeriodMonth { get => DatePeriodStart.Month.ToString(); }
        public string PeriodYear { get => DatePeriodStart.Year.ToString(); }
        /// <summary> Дата отчета (если с 1 по 15 то Аванс, если с 16 и выше то Расчет). В одном месяце только два отчета: Аванс и Расчет </summary>
        public DateTime DatePeriodStart
        {
            get => (DateAdd.Day < 16) ?
            new DateTime(DateAdd.Year, DateAdd.Month, 1) :
            new DateTime(DateAdd.Year, DateAdd.Month, 16);
        }
    }
}
