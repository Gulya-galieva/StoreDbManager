using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StoreDbManager.QueryModels;

namespace DbManager
{
    public interface ILinkData
    {
        string PhoneNumber { get; set; }
    }
    public class StoreContext : DbContext
    {
        //const string connectionString = "Server=tcp:92.50.142.178,11000; Database=Store; User Id=sa; Password=E3aCnW8lPi;"; //Через внешний IP
        const string connectionString = "Server=tcp:192.168.0.142; Database=Store; User Id=sa; Password=E3aCnW8lPi;"; //Через локальный IP
        
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }
        public StoreContext() : base() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
            optionsBuilder.UseLazyLoadingProxies();
        }
  
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Важный момент, тут задается уникальность поля заводской номе ПУ
            modelBuilder.Entity<Device>().HasIndex(d => d.SerialNumber).IsUnique();
        }
        
        //Склад//
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<MaterialUnit> MaterialUnits { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialDelivery> MaterialDeliveries { get; set; }
        public DbSet<DeviceDelivery> DeviceDeliveries { get; set; }
      
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }

        public DbSet<Worker> Workers { get; set; }
        public DbSet<WorkerType> WorkerTypes { get; set; }
        public DbSet<EmploymentContract> EmploymentContracts { get; set; }

        public DbSet<DeliveryAct> DeliveryActs { get; set; }
        public DbSet<DeliveryState> DeliveryStates { get; set; }
        public DbSet<DeliveryType> DeliveryTypes { get; set; }

        public DbSet<PPE> PPEs { get; set; }
        public DbSet<PPEDelivery> PPEDeliveries { get; set; }

        public DbSet<DeviceState> DeviceStates { get; set; }
        public DbSet<DeviceStateType> DeviceStateTypes { get; set; }
        
        //Пользователи
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        //Башакт//
        public DbSet<ActionType> ActionTypes { get; set; }              //Actions
        public DbSet<Link> Links { get; set; }
        public DbSet<SubstationLink> SubstationLinks { get; set; } //Link для подстанций 
        public DbSet<CommentRegPoint> CommentRegPoints { get; set; }
        public DbSet<RegPointAction> RegPointActions { get; set; }      //Actions
        public DbSet<RegPoint> RegPoints { get; set; }
        public DbSet<RegPointFlags> RegPointFlags { get; set; }
        //public DbSet<InstallActType> InstallActTypes { get; set; } мешается (не нужен) "Vladimir 01.03.2019"
        public DbSet<InstallAct> InstallActs { get; set; }
        public DbSet<InstallPlaceType> InstallPlaceTypes { get; set; }
        public DbSet<Consumer> Consumers { get; set; }
        public DbSet<Letter> Letters { get; set; }
        public DbSet<CommentSubstation> CommentSubstations { get; set; }
        public DbSet<UnreadSubstationComment> UnreadSubstationComments { get; set; }
        public DbSet<SubstationState> SubstationStates { get; set; }
        public DbSet<SubstationAction> SubstationActions { get; set; }  //Actions
        public DbSet<Substation> Substations { get; set; }
        public DbSet<NetRegion> NetRegions { get; set; }
        public DbSet<NetRegionAction> NetRegionActions { get; set; }  //Actions
        public DbSet<SubstationDevice> SubstationDevices { get; set; } //Оборудование на подстанции

        //Отчеты на оплату
        public DbSet<PaymentReport> PaymentReports { get; set; }  //
        public DbSet<PaymentReportRegPoint> PaymentReportRegPoints { get; set; }  //


        //Монтаж//
        public DbSet<MounterReportUgesAL> MounterReportUgesALs { get; set; }
        public DbSet<MounterReportUgesDeviceItem> MounterReportUgesALItems { get; set; }
        public DbSet<PowerLineSupport> PowerLineSupports { get; set; }
        public DbSet<KDE> KDEs { get; set; }
        public DbSet<KDEType> KDETypes { get; set; }
        public DbSet<ReportState> ReportStates { get; set; }
        public DbSet<ReportRemark> ReportRemarks { get; set; }
        public DbSet<ReportType> ReportTypes { get; set; }
        public DbSet<SBReport> SBReports { get; set; }
        public DbSet<Switch> Switches { get; set; }
        public DbSet<USPDReport> USPDReports { get; set; }
        public DbSet<AdditionalMaterial> AdditionalMaterials { get; set; }
        public DbSet<ReportComment> ReportComments { get; set; }
        public DbSet<UnmountReport> UnmountReports { get; set; }
        public DbSet<UnmountedDevice> UnmountedDevices { get; set; }


        //Запросы
        public DbQuery<SubstationRowQueryModel> SubstationRowQuery { get; set; }
    }

    #region  Склад
    public class Contract
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Material> Materials { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<DeliveryAct> DeliveryActs { get; set; }
        public virtual List<NetRegion> NetRegions { get; set; }

        public Contract()
        {
            Materials = new List<Material>();
            Devices = new List<Device>();
            DeliveryActs = new List<DeliveryAct>();
            NetRegions = new List<NetRegion>();
        }
    }

    public class DeliveryType
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual ICollection<DeliveryAct> DeliveryActs { get; set; }
        public DeliveryType()
        {
            DeliveryActs = new List<DeliveryAct>();
        }

    }

    public class DeliveryState
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual ICollection<DeliveryAct> DeliveryActs { get; set; }
        public DeliveryState()
        {
            DeliveryActs = new List<DeliveryAct>();
        }
    }

    public class DeliveryAct
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int DeliveryTypeId { get; set; }
        [Required]
        public int DeliveryStateId { get; set; }
        public int? ContractId { get; set; }
        public int? WorkerId { get; set; }
        public string SetName { get; set; }
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual DeliveryType DeliveryType { get; set; }
        public virtual DeliveryState DeliveryState { get; set; }
        public virtual Contract Contract { get; set; }
        public virtual Worker Worker { get; set; }

        public virtual ICollection<MaterialDelivery> MaterialDeliveries { get; set; }
        public virtual ICollection<DeviceDelivery> DeviceDeliveries { get; set; }
        public virtual ICollection<PPEDelivery> PPEDeliveries { get; set; }

        public DeliveryAct()
        {
            MaterialDeliveries = new List<MaterialDelivery>();
            DeviceDeliveries = new List<DeviceDelivery>();
        }
    }

    public class MaterialDelivery
    {
        public int Id { get; set; }
        public int DeliveryActId { get; set; }
        public int MaterialTypeId { get; set; }
        public int? MaterialId { get; set; }
        public double Volume { get; set; }
        public string Other { get; set; }
        public virtual DeliveryAct DeliveryAct { get; set; }
        public virtual MaterialType MaterialType { get; set; }
        public virtual Material Material { get; set; }
    }

    public class DeviceDelivery
    {
        public int Id { get; set; }
        public int DeliveryActId { get; set; }
        public int? DeviceTypeId { get; set; }
        public int? DeviceId { get; set; }
        public int? SetId { get; set; }
        public string SerialNumber { get; set; }
        public string Comment { get; set; }
        public virtual DeliveryAct DeliveryAct { get; set; }
        public virtual DeviceType DeviceType { get; set; }
        public virtual Device Device { get; set; }
    }

    public class Device
    {
        public int Id { get; set; }
        [Required]
        public string SerialNumber { get; set; }
        public int DeviceTypeId { get; set; }

        public string CurrentState { get; set; }
        [Required]
        public int ContractId { get; set; }

        public int? SetId { get; set; }
        [ForeignKey ("FK_Device_KDE_KDEId")]
        public int? KDEId { get; set; }
        public virtual KDE KDE { get; set; }
        public virtual Contract Contract { get; set; }
        public virtual DeviceType DeviceType { get; set; }
        public virtual ICollection<DeviceDelivery> DeviceDeliveries { get; set; }
        public virtual ICollection<DeviceState> DeviceStates { get; set; }
        public virtual ICollection<AdditionalMaterial> AdditionalMaterials { get; set; }
        public virtual List<Link> Links { get; set; }
        public virtual List<RegPoint> RegPoints { get; set; }
        public virtual List<MounterReportUgesDeviceItem> MounterReportUgesDeviceItems { get; set; }

        public Device()
        {
            DeviceDeliveries = new List<DeviceDelivery>();
            DeviceStates = new List<DeviceState>();
            AdditionalMaterials = new List<AdditionalMaterial>();
            Links = new List<Link>();
            RegPoints = new List<RegPoint>();
            MounterReportUgesDeviceItems = new List<MounterReportUgesDeviceItem>();

        }
    }

    public class DeviceType
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(10)]
        public string INom { get; set; }

        [MaxLength(10)]
        public string UNom { get; set; }

        [MaxLength(10)]
        public string EnergyType { get; set; }

        [MaxLength(10)]
        public string AccuracyClass { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        [MaxLength(100)]
        public string Type { get; set; }

        public int TestInterval { get; set; }

        [Required]
        [MaxLength(100)]
        public string ModelCode { get; set; }

        [Required]
        public double Price { get; set; }

        public virtual ICollection<Device> Devices { get; set; }

        public DeviceType()
        {
            Devices = new List<Device>();
        }
    }

    public class MaterialType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int MaterialUnitId { get; set; }
        public virtual MaterialUnit Unit { get; set; }
        public virtual ICollection<Material> Materials { get; set; }
        public double Price { get; set; } 

        public MaterialType()
        {
            Materials = new List<Material>();

        }
    }

    public class MaterialUnit
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string Name { get; set; }
    }

    public class Material
    {
        public int Id { get; set; }
        [Required]
        public int MaterialTypeId { get; set; }
        public double Volume { get; set; }
        public int? ContractId { get; set; }
        public virtual Contract Contract { get; set; }
        public virtual MaterialType MaterialType { get; set; }
        public virtual ICollection<MaterialDelivery> MaterialDeliveries { get; set; }
        public Material()
        {
            MaterialDeliveries = new List<MaterialDelivery>();
        }

    }

    public class Worker
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        [MaxLength(50)]
        public string MIddlename { get; set; }
        [Required]
        public int WorkerTypeId { get; set; }
        public bool DeliveryAvailible { get; set; }

        public virtual WorkerType WorkerType { get; set; }
        public virtual IEnumerable<DeliveryAct> DeliveryActs { get; set; }
        /// <summary> Отчеты на оплату </summary>
        public virtual IEnumerable<PaymentReport> PaymentReports { get; set; }
        public Worker()
        {
            DeliveryActs = new List<DeliveryAct>();
            PaymentReports = new List<PaymentReport>();
        }
    }

    public class WorkerType
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        public virtual ICollection<Worker> Workers { get; set; }
        public WorkerType()
        {
            Workers = new List<Worker>();
        }
    }

    public class PPEDeliveryAct
    {
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int DeliveryTypeId { get; set; }
        [Required]
        public int DeliveryStateId { get; set; }
        public int? WorkerId { get; set; }
        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual DeliveryType DeliveryType { get; set; }
        public virtual DeliveryState DeliveryState { get; set; }
        public virtual Worker Worker { get; set; }
    }

    public class PPE //Инструмент
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Volume { get; set; }
        public virtual PPEDelivery PEDeliveries { get; set; }
    }

    public class PPEDelivery
    {
        public int Id { get; set; }
        [ForeignKey("PPE")]
        public int? PPEId { get; set; }
        public int DeliveryActId { get; set; }
        public int Volume { get; set; }
        public virtual PPE PPE { get; set; }
        public virtual DeliveryAct DeliveryAct { get; set; }
    }

    public class DeviceState
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }

        public DateTime Date { get; set; }

        public int DeviceStateTypeId { get; set; }
        public virtual DeviceStateType DeviceStateType { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public string Comment { get; set; }

    }

    public class DeviceStateType
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class EmploymentContract
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int WorkerId { get; set; }
        public virtual Worker Worker { get; set; }
    }

    #endregion

    #region Пользователи
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public int? WorkerID { get; set; }
        public virtual Role Role { get; set; }
    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public Role()
        {
            Users = new List<User>();
        }
    }
    #endregion

    #region ЛК монтажника
    /// <summary>
    /// Коды ошибок иморта ТУ
    /// </summary>
    public enum MounterReportImportError
    {
        /// <summary>
        /// Нет ошибок
        /// </summary>
        None,
        /// <summary>
        /// Устройство уже привязано к активной ТУ
        /// </summary>
        ExistingRegPoint,
        /// <summary>
        /// Оборудование не найдено в БД по серийному номеру
        /// </summary>
        DeviceNotFound,
        /// <summary>
        /// Текущий статус ПУ не позвляет произвести импорт
        /// </summary>
        WrongState
    }

    public class MounterReportUgesAL
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string WorkPermit { get; set; }
        public string Fider { get; set; }
        public string Brigade { get; set; }
        public string Substation { get; set; }
        public string Local { get; set; }
        public int ContractId { get; set; }
        public virtual Contract Contract { get; set; }

        public int NetRegionId { get; set; }
        public virtual NetRegion NetRegion { get; set; }
        public int WorkerId { get; set; }
        public virtual Worker Worker {get;set;}

        public int ReportStateId { get; set; }
        public virtual ReportState ReportState { get; set; }

        public virtual List<PowerLineSupport> PowerLineSupports { get; set; }

        public int? CuratorId { get; set; }

        public MounterReportUgesAL()
        {
            PowerLineSupports = new List<PowerLineSupport>();
        }
    }

    public class MounterReportUgesDeviceItem
    {
        public int Id { get; set; }

        public int WorkerId { get; set; }
        public virtual Worker Worker { get; set; }

        public int? KDEId { get; set; }
        public virtual KDE KDE { get; set; }
        public string PowerLineType { get; set; }

        public string Street { get; set; }
        public string House { get; set; }
        public string Building { get; set; }
        public string Flat { get; set; }
        public string InstallPlace { get; set; }

        public string Serial { get; set; }
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }
        public string DeviceSeal { get; set; }
        public double Sum { get; set; }
        public double T1 { get; set; }
        public double T2 { get; set; }
        public double U1 { get; set; }
        public double U2 { get; set; }
        public double U3 { get; set; }

        public double WireConsumptionUpDown { get; set; }
        public double WireConsumptionNewInput { get; set; }

        public string KDEType { get; set; }
        public string KDESeal { get; set; }
        public string SwitchSeal { get; set; }
        public string PhoneNumber { get; set; }

        public MounterReportImportError ErrorCode { get; set; }
    }

    public class PowerLineSupport
    {
        public int Id { get; set; }
        public int SupportNumber { get; set; }
        public string PowerLineType { get; set; }
        public int FixatorsCount { get; set; }

        public int MounterReportUgesALId { get; set; }
        public virtual MounterReportUgesAL MounterReportUgesAL { get; set; }

        public virtual List<KDE> KDEs { get; set; }

        public PowerLineSupport()
        {
            KDEs = new List<KDE>();
        }
    }

    public class KDE
    {
        public int Id { get; set; }

        public int KDETypeId { get; set; }
        //public int DeviceId { get; set; }
        public virtual KDEType KDEType { get; set; }

        public int? PowerLineSupportId { get; set; }
        public virtual PowerLineSupport PowerLineSupport { get; set; }

        public virtual ICollection<MounterReportUgesDeviceItem> MounterReportUgesDeviceItems { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public KDE()
        {
            MounterReportUgesDeviceItems = new List<MounterReportUgesDeviceItem>();
            Devices = new List<Device>();
        }
    }

    public class ReportState
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class KDEType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ReportType
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class ReportRemark
    {
        public int Id { get; set; }
        public string Text { get; set; }
        [ForeignKey("FK_ReportRemarks_Users_UserId")]
        public int UserId { get; set; }
        public int ReportId { get; set; }
        public virtual User User {get; set;}
        [ForeignKey("FK_ReportRemarks_ReportTypes_ReportTypeId")]
        public int ReportTypeId { get; set; }
        public virtual ReportType ReportType { get; set; }
    }

    public class SBReport
    {
        public int Id { get; set; }
        public string Substation { get; set; }
        public string PhoneNumber { get; set; }
        public string MeterBoard { get; set; }
        public string Local { get; set; }

        public int ContractId { get; set; }
        public virtual Contract Contract { get; set; }

        public int NetRegionId { get; set; }
        public virtual NetRegion NetRegion { get; set; }
        public int WorkerId { get; set; }
        public virtual Worker Worker { get; set; }

        public DateTime Date { get; set; }
        public string WorkPermit { get; set; }
        public string Brigade { get; set; }
        public string Phase { get; set; }

       
        public int ReportStateId { get; set; }
        public virtual ReportState ReportState { get; set; }

        public int? CuratorId { get; set; }

        public virtual List<Switch> Switches { get; set; }
        public SBReport()
        {
            Switches = new List<Switch>();
        }
    }

    public class Switch
    {
        public int Id { get; set; }
        public string SwitchType { get; set; }
        public string SwitchNumber { get; set; }
        public string DeviceSerial { get; set; }
        public string DeviceSeal { get; set; }
        public string TestBoxSeal { get; set; }

        public double KVVG { get; set; }
        public int TTAk { get; set; }
        public string TTANumber { get; set; }
        public string TTASeal { get; set; }

        public int TTBk { get; set; }
        public string TTBNumber { get; set; }
        public string TTBSeal { get; set; }

        public int TTCk { get; set; }
        public string TTCNumber { get; set; }
        public string TTCSeal { get; set; }

        public double Sum { get; set; }
        public double T1 { get; set; }
        public double T2 { get; set; }

        public int? SBReportId { get; set; }
        public int? USPDReportId { get; set; }

        public virtual SBReport SBReport { get; set; }
        public virtual USPDReport USPDReport { get; set; }

        public MounterReportImportError ErrorCode { get; set; }
    }

    public class USPDReport
    {
        public int Id { get; set; }

        public int ContractId { get; set; }
        public virtual Contract Contract { get; set; }

        public int NetRegionId { get; set; }
        public virtual NetRegion NetRegion { get; set; }
        public int WorkerId { get; set; }
        public virtual Worker Worker { get; set; }
        public DateTime Date { get; set; }

        public string Substation { get; set; }
        public string PhoneNumber { get; set; }
        public string Local { get; set; }
        public string UspdSerial { get; set; }
        public string PlcSerial { get; set; }
        public bool AVR { get; set; }
        public string Brigade { get; set; }

        public double Kvvg { get; set; }
        public double Gofr { get; set; }
        public double Ftp { get; set; }

        public int? CuratorId { get; set; }
        public int ReportStateId { get; set; }
        public virtual ReportState ReportState { get; set; }

        public virtual List<Switch> Switches { get; set; }
        public USPDReport()
        {
            Switches = new List<Switch>();
        }

    }

    public class AdditionalMaterial //Дополнительные материалы (привязываются к ПУ)
    {
        public int Id { get; set; }
        [Required]
        public double Volume { get; set; }

        public int MaterialId { get; set; }
        public virtual Material Material { get; set; }

        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }
    }

    public class ReportComment //Комментарии к отчету
    {
        public int Id { get; set; }
        public int ReportTypeId { get; set; }
        public virtual ReportType ReportType { get; set; }
        public int ReportId { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }

    public class UnmountReport //Отчет по демонтажу
    {
        public int Id { get; set; }

        public int ContractId { get; set; }
        public virtual Contract Contract {get; set;}

        public int? NetRegionId { get; set; }
       

        public int WorkerId { get; set; }
        public virtual Worker Worker { get; set; }

        public DateTime Date { get; set; }

        public int? CuratorId { get; set; }
        public int ReportStateId { get; set; }
        public virtual ReportState ReportState { get; set; }

        public virtual ICollection<UnmountedDevice> UnmountedDevices { get; set; }
        public UnmountReport()
        {
            UnmountedDevices = new List<UnmountedDevice>();
        }

    }

    public class UnmountedDevice //Демонтированное оборудование
    {
        public int Id { get; set; }
        public string Reason { get; set; }

        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }

        public int UnmountReportId { get; set; }
        public virtual UnmountReport UnmountReport {get;set;}
    }

    #endregion

    #region БашАкт
    /// <summary>
    /// Сущность Номер сим и тип модема для ПУ
    /// </summary>
    public class Link : ILinkData
    {
        public int Id { get; set; }

        public string LinkType { get; set; } //Внешний GSM модем, встроенныей в счетчик модем и прочее
        public string PhoneNumber { get; set; }

        //FK
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }
    }

    /// <summary>
    /// Сущность Номер сим для ТП
    /// </summary>
    public class SubstationLink : ILinkData
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        
        //FK
        public int SubstationId { get; set; }
        public virtual Substation Substation { get; set; }
    }

    /// <summary>
    /// Сущность комментарии к точке учета
    /// </summary>
    public class CommentRegPoint
    {
        public int Id { get; set; }
        public string Text { get; set; }
        //FK
        public virtual RegPoint RegPoint { get; set; }
        public int RegPointId { get; set; }
    }
    /// <summary>
    /// Сущность - тип действия совершаемого пользователем. Содержит в себе имя действия типа string.
    /// </summary>
    public class ActionType
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    
    /// <summary>
    /// Сущность - Действие совершаемое пользователем на точкой учета. Содержит дату действия, тип действия, пользователя [и комментарий].
    /// </summary>
    public class RegPointAction
    {
        [Key]
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }

        //Тип действия FK
        public int ActionTypeId { get; set; }
        public virtual ActionType ActionType { get; set; }

        //Точка учета FK
        public int RegPointId { get; set; }
        public virtual RegPoint RegPoint { get; set; }

        //Пользователь FK
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }

    /// <summary>Статус точки учета </summary>
    public enum RegPointStatus
    {
        /// <summary> Обычная активная точка учета </summary>
        Default,
        /// <summary> Демонтированый ПУ </summary>
        Demounted,
        /// <summary> Точка учета для привязки УСПД к Подстанции </summary>
        USPD
    }

    /// <summary>
    /// Сущность - Точка учета. Связана с сущностями, которые содержат всю информацию о точке учета, акте, потребителе, приборе учета и тд.
    /// </summary>
    public class RegPoint
    {
        [Key]
        public int Id { get; set; }

        /// <summary> Статус точки учета </summary>
        [DefaultValue(0)]
        public RegPointStatus Status { get; set; }
        //Объект (подстанция)
        public int SubstationId { get; set; }
        public virtual Substation Substation { get; set; }
        //Счетчик
        public int? DeviceId { get; set; }
        public virtual Device Device { get; set; }

        //Потребитель (1к1)
        public virtual Consumer Consumer { get; set; }
        //Инфа для акта допуска (1к1)
        public virtual InstallAct InstallAct { get; set; }
        //Флаги состояний (распечатан акт, письмо и т.д.) (1к1)
        public virtual RegPointFlags RegPointFlags { get; set; }

        public virtual List<Letter> Letters { get; set; }
        public virtual List<CommentRegPoint> Comments { get; set; }
        public virtual List<RegPointAction> RegPointActions { get; set; }

        //Связь с отчетами на оплату
        public virtual List<PaymentReportRegPoint> PaymentReportRegPoints { get; set; }

        public RegPoint()
        {
            //Инициализируем списки
            Letters = new List<Letter>();
            Comments = new List<CommentRegPoint>();
            RegPointActions = new List<RegPointAction>();
            PaymentReportRegPoints = new List<PaymentReportRegPoint>();
        }
    }

    /// <summary>
    /// Сущность - Флаги точки учета. Содержит флаги описывающие состояния точки учета.
    /// </summary>
    public class RegPointFlags
    {
        public int Id { get; set; }
        /// <summary>Id точки учета, к которой относятся эти флаги</summary>
        public int RegPointId { get; set; }
        /// <summary>Точка учета, к которой относятся эти флаги</summary>
        public virtual RegPoint RegPoint { get; set; }

        /// <summary>Данные о потребителе предоставлены из вне (или импортированы)</summary>
        public bool ImportConsummerData { get; set; }        //Данные импортированы (по портебителям)
        /// <summary>Акт на существующий ПУ, которого нет в базе</summary>
        public bool ExsistRegDevice { get; set; }   //Существующий прибор учета (не надо привязывать Device)
        /// <summary>ТУ создана из отчета монтажника</summary>
        public bool ReportedByMounter { get; set; } //ТУ заведена монтажником

        //Для АСКУЭ
        /// <summary>Есть связь с ПУ (первоначальная проверка после ПНР)</summary>
        [DefaultValue(false)]
        public bool IsLinkOk { get; set; }
        /// <summary>ПУ повторно проверен и будет заведён в ПО АСКУЭ</summary>
        [DefaultValue(false)]
        public bool IsAscueChecked { get; set; }
        /// <summary>ПУ работает в ПО АСКУЭ</summary>
        [DefaultValue(false)]
        public bool IsAscueOk { get; set; }
        /// <summary>Замена ПУ</summary>
        [DefaultValue(false)]
        public bool IsReplace { get; set; }

        /// <summary>Данные из ЭСКБ корректны</summary>
        [DefaultValue(false)]
        public bool IsDataConfirmed { get; set; }

        /// <summary>
        /// Копировать значения всех полей из другого объекта
        /// </summary>
        /// <param name="flagsFrom">Объект из которого будут скопированы все поля</param>
        public void CopyDataFrom(RegPointFlags flagsFrom)
        {
            Id = flagsFrom.Id;

            RegPointId = flagsFrom.RegPointId;

            ImportConsummerData = flagsFrom.ImportConsummerData;
            ExsistRegDevice = flagsFrom.ExsistRegDevice;
            ReportedByMounter = flagsFrom.ReportedByMounter;
        }
    }

    /// <summary>
    /// Отчеты для оплаты работ по СМР
    /// </summary>
    public class PaymentReport
    {
        /// <summary> Id отчета на оплату СМР </summary>
        public int Id { get; set; }

        /// <summary> Если отчет закрыт, то его нельзя редактировать (он оплачен) </summary>
        public DateTime DateCreate { get; set; }
        /// <summary> Дата начала периода отчета (1.02 или 15.02, первая или вторая половина месяца </summary>
        public DateTime DatePeriodStart { get; set; }

        /// <summary> Если отчет закрыт, то его нельзя редактировать (он оплачен) </summary>
        [DefaultValue(false)]
        public bool IsClosed { get; set; }
        /// <summary> Список устройств прикрепленных к отчету </summary>
        public virtual List<PaymentReportRegPoint> PaymentReportRegPoints { get; set; }
        /// <summary> Коментарий к отчету (любой) </summary>
        public string Comment { get; set; }
        /// <summary> Id работника, по которому отчет </summary>
        public int WorkerId { get; set; }
        /// <summary> Работник, по которому отчет </summary>
        public virtual Worker Worker { get; set; }

        public PaymentReport()
        {
            PaymentReportRegPoints = new List<PaymentReportRegPoint>();
        }
    }

    /// <summary> Тип работы произведенной монтажником </summary>
    public enum PaymentReportWorkType
    {
        /// <summary> Не определено </summary>
        [EnumMember(Value = "-")]
        [Display(Name = "-")]
        None = 0,
        /// <summary> Монтаж </summary>
        [EnumMember(Value = "Монтаж")]
        [Display(Name = "Монтаж")]
        Mounting = 1,
        /// <summary> Демонтаж </summary>
        [EnumMember(Value = "Демонтаж")]
        [Display(Name = "Демонтаж")]
        Demounting = 2,
        /// <summary> Пуско-наладка </summary>
        [EnumMember(Value = "Пуско-наладка")]
        [Display(Name = "Пуско-наладка")]
        PNR = 3
    }

    /// <summary> Связь между точками учета и отчетами на оплату </summary>
    public class PaymentReportRegPoint
    {
        public int Id { get; set; }
        /// <summary> Точка учета, которая привязывается к отчету </summary>
        public int RegPointId { get; set; }
        /// <summary> Точка учета, которая привязывается к отчету </summary>
        public virtual RegPoint RegPoint { get; set; }

        /// <summary> Отчет к которому привязывается устройство </summary>
        public int PaymentReportId { get; set; }
        /// <summary> Отчет к которому привязывается устройство </summary>
        public virtual PaymentReport PaymentReport { get; set; }

        /// <summary> Тип работы произведенной монтажником </summary>
        [DefaultValue(0)]
        public PaymentReportWorkType WorkType { get; set; }
        /// <summary> Цена за СМР ПУ </summary>
        public double CostRUB { get; set; }
    }

    /// <summary> Тип акта не задан</summary>
    public enum InstallActType
    {
        /// <summary> Тип акта не задан </summary>
        [EnumMember(Value = "-")]
        None = 0,
        /// <summary> Акт с потребителем </summary>
        [EnumMember(Value = "Акт с потребителем")]
        Consummer = 1,
        /// <summary> Акт без потребителя </summary>
        [EnumMember(Value = "Акт без потребителя")]
        NoComsummer = 2,
        /// <summary>ВЛ (воздушные линии) </summary>
        [EnumMember(Value = "ВЛ (воздушные линии)")]
        VL = 3
    }

    /// <summary>
    /// Сущность - Акт. Содержит основную информацию для акта допуска.
    /// </summary>
    public class InstallAct
    {
        /// <summary> Id </summary>
        [Key]
        public int Id { get; set; }
        //FK (1к1)
        /// <summary> Id точки учета </summary>
        public int RegPointId { get; set; }
        /// <summary> Навигационное свойсво </summary>
        public virtual RegPoint RegPoint { get; set; }

        /// <summary>
        /// Тип учета (true - Коммерческий учет, false - технический учет)
        /// </summary>
        public bool IsCommReg { get; set; }

        //Шапка акта
        /// <summary> Подстанция (высокая сторона) название (вообще вроде не нужно ???) </summary>
        public string PS { get; set; }
        /// <summary> Линия (обычно номер 1, 2 и тд.) </summary>
        public string Line { get; set; }
        /// <summary> Фидер (обычно номер 1, 2 и тд.) </summary>
        public string Feeder { get; set; }
        /// <summary> Секция (обычно номер 1 или 2) </summary>
        public string Section { get; set; }
        /// <summary> Переток (true - Отдача (Ввод), false - Прием (ВЛ и др) )  </summary>
        public bool IsOutFlow { get; set; }
        /// <summary> Место установки - Номер (Ввод №1 или Рубильник №7 или Опора №3) </summary>
        public int? InstallPlaceNumber { get; set; }    //Место установки номер (Ввод №1 или Рубильник №7 или Опора №3)
        /// <summary> Место установки - Id Типа (Ввод, Рубильник или Опора) </summary>
        public int InstallPlaceTypeId { get; set; }    //Тип Места установки (Ввод или Рубильник или Опора тд)
        /// <summary> Место установки - Тип (Ввод, Рубильник или Опора) </summary>
        public virtual InstallPlaceType InstallPlaceType { get; set; }

        //Демонтированный ПУ
        /// <summary> Серийник </summary>
        public string Uninstalled_Serial { get; set; }
        /// <summary> Последние показания </summary>
        public string Uninstalled_TSum { get; set; }
        /// <summary> Коэффициент трансформатора тока </summary>
        public string Uninstalled_Ktt { get; set; }

        //Показания (установленный счетчик)
        /// <summary> Суммарный расход (поле в акте) </summary>
        public string Tsum { get; set; }
        /// <summary>  Расход по первому тарифу (поле в акте) </summary>
        public string T1 { get; set; }
        /// <summary>  Расход по второму тарифу (поле в акте) </summary>
        public string T2 { get; set; }

        //ТТ серийник
        /// <summary> Серийник </summary>
        public string TT_A_Serial { get; set; }
        /// <summary> Серийник </summary>
        public string TT_B_Serial { get; set; }
        /// <summary> Серийник </summary>
        public string TT_C_Serial { get; set; }
        //Ктт
        /// <summary> Серийник </summary>
        public string TT_Koefficient { get; set; }

        //Пломбы
        /// <summary> Номер пломбы ТТ фаза A </summary>
        public string Seal_TT_A { get; set; }
        /// <summary> Номер пломбы ТТ фаза B </summary>
        public string Seal_TT_B { get; set; }
        /// <summary> Номер пломбы ТТ фаза C </summary>
        public string Seal_TT_C { get; set; }
        /// <summary> Номер пломбы на счетчике </summary>
        public string Seal_RegDevice { get; set; }
        /// <summary> Номер пломбы КИ (клемма испытательная) </summary>
        public string Seal_KI { get; set; }
        /// <summary> Номер пломбы КДЕ </summary>
        public string Seal_KDE { get; set; }
        /// <summary> Номер пломбы автомата 1 </summary>
        public string Seal_AV1 { get; set; }
        /// <summary> Номер пломбы автомата 2 </summary>
        public string Seal_AV2 { get; set; }

        /// <summary> Тип акта (ВЛ, с потребителем, без потребителя) </summary>
        public InstallActType InstallActType { get; set; }

        /// <summary>
        /// Копировать значения всех полей из другого объекта
        /// </summary>
        /// <param name="installActFrom">Объект из которого будут скопированы все поля</param>
        public void CopyDataFrom(InstallAct installActFrom)
        {
            if (installActFrom == null) return;

            Id = installActFrom.Id;
            RegPointId = installActFrom.RegPointId;

            //Тип учета
            IsCommReg = installActFrom.IsCommReg;

            //Шапка акта
            PS = installActFrom.PS;
            Line = installActFrom.Line;
            Feeder = installActFrom.Feeder;
            Section = installActFrom.Section;
            InstallPlaceNumber = installActFrom.InstallPlaceNumber;
            InstallPlaceTypeId = installActFrom.InstallPlaceTypeId;
            InstallPlaceType = installActFrom.InstallPlaceType;
            IsOutFlow = installActFrom.IsOutFlow;

            //Демонтированный ПУ
            Uninstalled_Serial = installActFrom.Uninstalled_Serial;
            Uninstalled_TSum = installActFrom.Uninstalled_TSum;
            Uninstalled_Ktt = installActFrom.Uninstalled_Ktt;

            //Показания (установленный счетчик)
            Tsum = installActFrom.Tsum;
            T1 = installActFrom.T1;
            T2 = installActFrom.T2;

            //ТТ серийник
            TT_A_Serial = installActFrom.TT_A_Serial;
            TT_B_Serial = installActFrom.TT_B_Serial;
            TT_C_Serial = installActFrom.TT_C_Serial;

            //Ктт
            TT_Koefficient = installActFrom.TT_Koefficient;

            //Пломбы
            Seal_TT_A = installActFrom.Seal_TT_A;
            Seal_TT_B = installActFrom.Seal_TT_B;
            Seal_TT_C = installActFrom.Seal_TT_C;
            Seal_RegDevice = installActFrom.Seal_RegDevice;
            Seal_KI = installActFrom.Seal_KI;
            Seal_KDE = installActFrom.Seal_KDE;
            Seal_AV1 = installActFrom.Seal_AV1;
            Seal_AV2 = installActFrom.Seal_AV2;

            InstallActType = installActFrom.InstallActType;
        }
    }

    /// <summary>
    /// Сущность - Тип места установки. Содержит информацию о месте установки точки учета.
    /// </summary>
    public class InstallPlaceType
    {
        /// <summary>Id </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>Текстовое описание места установки (заканчивается на №)</summary>
        public string Name { get; set; }
    }
    /// <summary>
    /// Сущность - Потребитель. Содержит информацию о потребителе, связанным с точкой учета.
    /// </summary>
    public class Consumer
    {
        [Key]
        public int Id { get; set; }
        //FK (1к1)
        public int RegPointId { get; set; }
        public virtual RegPoint RegPoint { get; set; }

        public string Name { get; set; }            //Полное имя (ФИО)
        public string ContractNumber { get; set; }  //Номер договора
        //Юр Адрес
        public string U_Index { get; set; } //Почтовый индекс
        public string U_Local { get; set; } //Населеный пункт
        public string U_Local_Secondary { get; set; } //Населеный 2
        public string U_Street { get; set; }//Улица
        public string U_House { get; set; } //Номер дома
        public string U_Build { get; set; } //Корпус
        public string U_Flat { get; set; }  //Квартира
        //Адрес объекта
        public string O_Index { get; set; } //Почтовый 
        public string O_Local { get; set; } //Населеный
        public string O_Local_Secondary { get; set; } //Населеный 2
        public string O_Street { get; set; }//Улица
        public string O_House { get; set; } //Номер дом
        public string O_Build { get; set; } //Корпус
        public string O_Flat { get; set; }  //Квартира

        /// <summary>
        /// Копировать значения всех полей из другого объекта
        /// </summary>
        /// <param name="consumerFrom">Объект из которого будут скопированы все поля</param>
        public void CopyDataFrom(Consumer consumerFrom)
        {
            Id = consumerFrom.Id;

            //FK (1к1)
            RegPointId = consumerFrom.RegPointId;

            Name = consumerFrom.Name;
            ContractNumber = consumerFrom.ContractNumber;

            //Юр Адрес
            U_Index = consumerFrom.U_Index;
            U_Local = consumerFrom.U_Local;
            U_Local_Secondary = consumerFrom.U_Local_Secondary;
            U_Street = consumerFrom.U_Street;
            U_House = consumerFrom.U_House;
            U_Build = consumerFrom.U_Build;
            U_Flat = consumerFrom.U_Flat;
            //Адрес объекта
            O_Index = consumerFrom.O_Index;
            O_Local = consumerFrom.O_Local;
            O_Local_Secondary = consumerFrom.O_Local_Secondary;
            O_Street = consumerFrom.O_Street;
            O_House = consumerFrom.O_House;
            O_Build = consumerFrom.O_Build;
            O_Flat = consumerFrom.O_Flat;
        }
    }

    /// <summary>
    /// Сущность - Письмо. Содержит информацию для писем приглашений на допуск ПУ.
    /// </summary>
    public class Letter
    {
        [Key]
        public int Id { get; set; }
        //FK
        public int RegPointId { get; set; }     //Точка учета
        public virtual RegPoint RegPoint { get; set; }

        public string OutNumber { get; set; }   //Исходящий номер письма

        public DateTime DateLetter { get; set; } //Дата письма (хз какая)
        public DateTime InviteDate { get; set; } //Дата приглашения
        public DateTime NotifyDate { get; set; } //Дата уведомления
        public DateTime HandingDate { get; set; } //Дата вручения
        //
        public bool Printed { get; set; }       //Распечатано
        public string TrackNumber { get; set; }    //Трек-номер

        /// <summary>
        /// Копировать значения всех полей из другого объекта
        /// </summary>
        /// <param name="letterFrom">Объект из которого будут скопированы все поля</param>
        public void CopyDataFrom(Letter letterFrom)
        {
            Id = letterFrom.Id;

            OutNumber = letterFrom.OutNumber;

            DateLetter = letterFrom.DateLetter;
            InviteDate = letterFrom.InviteDate;
            NotifyDate = letterFrom.NotifyDate;
            HandingDate = letterFrom.HandingDate;

            Printed = letterFrom.Printed;
            TrackNumber = letterFrom.TrackNumber;

            RegPointId = letterFrom.RegPointId;
            RegPoint = letterFrom.RegPoint;
        }
    }

    //Комментарий к подстанции
    public class CommentSubstation
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        //FK
        public virtual Substation Substation { get; set; }
        public int SubstationId { get; set; }

        public virtual User User { get; set; }
        public int UserId { get; set; }
    }

    //Непрочитанные комментарии
    public class UnreadSubstationComment
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int CommentSubstationId { get; set; }
        public virtual CommentSubstation CommentSubstation { get; set; }

    }

    //Статус подстанции
    public class SubstationState
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //FK
        public virtual List<Substation> Substation { get; set; }

        public SubstationState()
        {
            Substation = new List<Substation>();
        }
    }

    //Какие-то действия над подстанцией производимые пользователем
    public class SubstationAction
    {
        [Key]
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }

        //Тип действия FK
        public virtual ActionType ActionType { get; set; }
        public int ActionTypeId { get; set; }

        //Подстанция FK
        public virtual Substation Substation { get; set; }
        public int SubstationId { get; set; }

        //Пользователь FK
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }

   

	public class Substation
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        //FK
        public int NetRegionId { get; set; } //Район
        public virtual NetRegion NetRegion { get; set; }
        //FK
        public int SubstationStateId { get; set; } //Статус подстанции
        public virtual SubstationState SubstationState { get; set; }

        public virtual List<SubstationLink> SubstationLinks { get; set; }
        public virtual List<RegPoint> RegPoints { get; set; } //Точки учета
        public virtual List<CommentSubstation> Comments { get; set; } //Комментарии
        public virtual List<SubstationAction> SubstationActions { get; set; } //Комментарии

        /// <summary> СМР закончены </summary>
		[DefaultValue(false)]
        public bool IsInstallationDone { get; set; }
        /// <summary> Поопроные схемы готовы </summary>
        [DefaultValue(false)]
        public bool IsPropSchemeDone { get; set; }
        /// <summary> Баланс сведен </summary>
        [DefaultValue(false)]
        public bool IsBalanceDone { get; set; }

        public Substation()
        {
            RegPoints = new List<RegPoint>();
            Comments = new List<CommentSubstation>();
            SubstationActions = new List<SubstationAction>();
            SubstationLinks = new List<SubstationLink>();
        }
    }

    //Какие-то действия в Районе производимые пользователем
    public class NetRegionAction
    {
        [Key]
        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }

        //Тип действия FK
        public virtual ActionType ActionType { get; set; }
        public int ActionTypeId { get; set; }

        //Район FK
        public virtual NetRegion NetRegion { get; set; }
        public int NetRegionId { get; set; }

        //Пользователь FK
        public virtual User User { get; set; }
        public int UserId { get; set; }
    }

    public class NetRegion
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public string ChiefName { get; set; }
        public string District { get; set; }
        //FK
        public int ContractId { get; set; }
        public virtual Contract Contract { get; set; }

        public virtual List<Substation> Substations { get; set; }
        public virtual List<NetRegionAction> NetRegionActions { get; set; }

        public NetRegion()
        {
            Substations = new List<Substation>();
            NetRegionActions = new List<NetRegionAction>();
        }
    }

    /// <summary>
    ///Оборудованние установленное на подстанции
    /// </summary>
    public class SubstationDevice
    {
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int SubstationId { get; set; }

        public virtual Device Device { get; set; }
        public virtual Substation Substation { get; set; }
    }
    
    #endregion
}
