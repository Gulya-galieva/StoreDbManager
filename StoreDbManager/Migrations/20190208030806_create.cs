using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DbManager.Migrations
{
    public partial class create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consumers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ContractNumber = table.Column<string>(nullable: true),
                    U_Index = table.Column<string>(nullable: true),
                    U_Local = table.Column<string>(nullable: true),
                    U_Street = table.Column<string>(nullable: true),
                    U_House = table.Column<string>(nullable: true),
                    U_Build = table.Column<string>(nullable: true),
                    U_Flat = table.Column<string>(nullable: true),
                    O_Index = table.Column<string>(nullable: true),
                    O_Local = table.Column<string>(nullable: true),
                    O_Street = table.Column<string>(nullable: true),
                    O_House = table.Column<string>(nullable: true),
                    O_Build = table.Column<string>(nullable: true),
                    O_Flat = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consumers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceStateTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceStateTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    INom = table.Column<string>(maxLength: 10, nullable: true),
                    UNom = table.Column<string>(maxLength: 10, nullable: true),
                    EnergyType = table.Column<string>(maxLength: 10, nullable: true),
                    AccuracyClass = table.Column<string>(maxLength: 10, nullable: true),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Type = table.Column<string>(maxLength: 100, nullable: false),
                    TestInterval = table.Column<int>(nullable: false),
                    ModelCode = table.Column<string>(maxLength: 100, nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstallActTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallActTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KDETypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KDETypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialUnits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialUnits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PPEs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Volume = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PPEs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubstationStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubstationStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkerTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NetRegions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    ChiefName = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    ContractId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NetRegions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NetRegions_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SerialNumber = table.Column<string>(nullable: false),
                    DeviceTypeId = table.Column<int>(nullable: false),
                    CurrentState = table.Column<string>(nullable: true),
                    ContractId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Devices_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaterialTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    MaterialUnitId = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialTypes_MaterialUnits_MaterialUnitId",
                        column: x => x.MaterialUnitId,
                        principalTable: "MaterialUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Login = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    RoleId = table.Column<int>(nullable: true),
                    WorkerID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 20, nullable: true),
                    Surname = table.Column<string>(maxLength: 50, nullable: true),
                    MIddlename = table.Column<string>(maxLength: 50, nullable: true),
                    WorkerTypeId = table.Column<int>(nullable: false),
                    DeliveryAvailible = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workers_WorkerTypes_WorkerTypeId",
                        column: x => x.WorkerTypeId,
                        principalTable: "WorkerTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Substations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    DateAdd = table.Column<DateTime>(nullable: false),
                    NetRegionId = table.Column<int>(nullable: false),
                    SubstationStateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Substations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Substations_NetRegions_NetRegionId",
                        column: x => x.NetRegionId,
                        principalTable: "NetRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Substations_SubstationStates_SubstationStateId",
                        column: x => x.SubstationStateId,
                        principalTable: "SubstationStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LinkType = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    DeviceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Links_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaterialTypeId = table.Column<int>(nullable: false),
                    Volume = table.Column<double>(nullable: false),
                    ContractId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Materials_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Materials_MaterialTypes_MaterialTypeId",
                        column: x => x.MaterialTypeId,
                        principalTable: "MaterialTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceStates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeviceId = table.Column<int>(nullable: false),
                    DeviceStateTypeId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceStates_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceStates_DeviceStateTypes_DeviceStateTypeId",
                        column: x => x.DeviceStateTypeId,
                        principalTable: "DeviceStateTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceStates_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryActs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    DeliveryTypeId = table.Column<int>(nullable: false),
                    DeliveryStateId = table.Column<int>(nullable: false),
                    ContractId = table.Column<int>(nullable: true),
                    WorkerId = table.Column<int>(nullable: true),
                    SetName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryActs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryActs_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeliveryActs_DeliveryStates_DeliveryStateId",
                        column: x => x.DeliveryStateId,
                        principalTable: "DeliveryStates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryActs_DeliveryTypes_DeliveryTypeId",
                        column: x => x.DeliveryTypeId,
                        principalTable: "DeliveryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryActs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryActs_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentContracts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    WorkerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmploymentContracts_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MounterReportUgesALs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    WorkPermit = table.Column<string>(nullable: true),
                    Fider = table.Column<string>(nullable: true),
                    Brigade = table.Column<string>(nullable: true),
                    Substation = table.Column<string>(nullable: true),
                    Local = table.Column<string>(nullable: true),
                    ContractId = table.Column<int>(nullable: false),
                    NetRegionId = table.Column<int>(nullable: false),
                    WorkerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MounterReportUgesALs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MounterReportUgesALs_Contracts_ContractId",
                        column: x => x.ContractId,
                        principalTable: "Contracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MounterReportUgesALs_NetRegions_NetRegionId",
                        column: x => x.NetRegionId,
                        principalTable: "NetRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_MounterReportUgesALs_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentSubstations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    SubstationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentSubstations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentSubstations_Substations_SubstationId",
                        column: x => x.SubstationId,
                        principalTable: "Substations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegPoints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PS = table.Column<string>(nullable: true),
                    Line = table.Column<string>(nullable: true),
                    Feeder = table.Column<string>(nullable: true),
                    Section = table.Column<string>(nullable: true),
                    InstallPlace = table.Column<string>(nullable: true),
                    Direction = table.Column<string>(nullable: true),
                    CommReg = table.Column<bool>(nullable: false),
                    SubstationId = table.Column<int>(nullable: false),
                    ConsumerId = table.Column<int>(nullable: false),
                    DeviceId = table.Column<int>(nullable: true),
                    RegPointFlagsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegPoints_Consumers_ConsumerId",
                        column: x => x.ConsumerId,
                        principalTable: "Consumers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegPoints_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegPoints_Substations_SubstationId",
                        column: x => x.SubstationId,
                        principalTable: "Substations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceDeliveries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeliveryActId = table.Column<int>(nullable: false),
                    DeviceTypeId = table.Column<int>(nullable: true),
                    DeviceId = table.Column<int>(nullable: true),
                    SerialNumber = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceDeliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceDeliveries_DeliveryActs_DeliveryActId",
                        column: x => x.DeliveryActId,
                        principalTable: "DeliveryActs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceDeliveries_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceDeliveries_DeviceTypes_DeviceTypeId",
                        column: x => x.DeviceTypeId,
                        principalTable: "DeviceTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MaterialDeliveries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeliveryActId = table.Column<int>(nullable: false),
                    MaterialTypeId = table.Column<int>(nullable: false),
                    MaterialId = table.Column<int>(nullable: true),
                    Volume = table.Column<double>(nullable: false),
                    Other = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialDeliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialDeliveries_DeliveryActs_DeliveryActId",
                        column: x => x.DeliveryActId,
                        principalTable: "DeliveryActs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaterialDeliveries_Materials_MaterialId",
                        column: x => x.MaterialId,
                        principalTable: "Materials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MaterialDeliveries_MaterialTypes_MaterialTypeId",
                        column: x => x.MaterialTypeId,
                        principalTable: "MaterialTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PPEDeliveries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PPEId = table.Column<int>(nullable: true),
                    DeliveryActId = table.Column<int>(nullable: false),
                    Volume = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PPEDeliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PPEDeliveries_DeliveryActs_DeliveryActId",
                        column: x => x.DeliveryActId,
                        principalTable: "DeliveryActs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PPEDeliveries_PPEs_PPEId",
                        column: x => x.PPEId,
                        principalTable: "PPEs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PowerLineSupports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SupportNumber = table.Column<int>(nullable: false),
                    PowerLineType = table.Column<string>(nullable: true),
                    FixatorsCount = table.Column<int>(nullable: false),
                    MounterReportUgesALId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerLineSupports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerLineSupports_MounterReportUgesALs_MounterReportUgesALId",
                        column: x => x.MounterReportUgesALId,
                        principalTable: "MounterReportUgesALs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentRegPoints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(nullable: true),
                    RegPointId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentRegPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentRegPoints_RegPoints_RegPointId",
                        column: x => x.RegPointId,
                        principalTable: "RegPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InstallActs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RegPointId = table.Column<int>(nullable: false),
                    Uninstalled_Serial = table.Column<string>(nullable: true),
                    Uninstalled_TSum = table.Column<string>(nullable: true),
                    Uninstalled_Ktt = table.Column<string>(nullable: true),
                    Tsum = table.Column<int>(nullable: false),
                    T1 = table.Column<int>(nullable: false),
                    T2 = table.Column<int>(nullable: false),
                    TT_A_Serial = table.Column<string>(nullable: true),
                    TT_B_Serial = table.Column<string>(nullable: true),
                    TT_C_Serial = table.Column<string>(nullable: true),
                    TT_Koefficient = table.Column<string>(nullable: true),
                    Seal_TT_A = table.Column<string>(nullable: true),
                    Seal_TT_B = table.Column<string>(nullable: true),
                    Seal_TT_C = table.Column<string>(nullable: true),
                    Seal_RegDevice = table.Column<string>(nullable: true),
                    Seal_KI = table.Column<string>(nullable: true),
                    Seal_KDE = table.Column<string>(nullable: true),
                    Seal_AV1 = table.Column<string>(nullable: true),
                    Seal_AV2 = table.Column<string>(nullable: true),
                    InstallActTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstallActs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstallActs_InstallActTypes_InstallActTypeId",
                        column: x => x.InstallActTypeId,
                        principalTable: "InstallActTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InstallActs_RegPoints_RegPointId",
                        column: x => x.RegPointId,
                        principalTable: "RegPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Letters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OutNumber = table.Column<string>(nullable: true),
                    DateLetter = table.Column<DateTime>(nullable: false),
                    InviteDate = table.Column<DateTime>(nullable: false),
                    NotifyDate = table.Column<DateTime>(nullable: false),
                    HandingDate = table.Column<DateTime>(nullable: false),
                    Printed = table.Column<bool>(nullable: false),
                    NumOfReestr = table.Column<int>(nullable: true),
                    RegPointId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Letters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Letters_RegPoints_RegPointId",
                        column: x => x.RegPointId,
                        principalTable: "RegPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegPointFlags",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RegPointId = table.Column<int>(nullable: false),
                    Accept = table.Column<bool>(nullable: false),
                    Notification = table.Column<bool>(nullable: false),
                    Import = table.Column<bool>(nullable: false),
                    Printed = table.Column<bool>(nullable: false),
                    Sent = table.Column<bool>(nullable: false),
                    Accepted = table.Column<bool>(nullable: false),
                    ExsistRegDevice = table.Column<bool>(nullable: false),
                    ReportedByMounter = table.Column<bool>(nullable: false),
                    SentPaper = table.Column<bool>(nullable: false),
                    SentActAccount = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegPointFlags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegPointFlags_RegPoints_RegPointId",
                        column: x => x.RegPointId,
                        principalTable: "RegPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KDEs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KDETypeId = table.Column<int>(nullable: false),
                    PowerLineSupportId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KDEs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KDEs_KDETypes_KDETypeId",
                        column: x => x.KDETypeId,
                        principalTable: "KDETypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KDEs_PowerLineSupports_PowerLineSupportId",
                        column: x => x.PowerLineSupportId,
                        principalTable: "PowerLineSupports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MounterReportUgesALItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WorkerId = table.Column<int>(nullable: false),
                    KDEId = table.Column<int>(nullable: true),
                    PowerLineType = table.Column<string>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    House = table.Column<string>(nullable: true),
                    Building = table.Column<string>(nullable: true),
                    Flat = table.Column<string>(nullable: true),
                    InstallPlace = table.Column<string>(nullable: true),
                    Serial = table.Column<string>(nullable: true),
                    DeviceId = table.Column<int>(nullable: false),
                    DeviceSeal = table.Column<string>(nullable: true),
                    Sum = table.Column<double>(nullable: false),
                    T1 = table.Column<double>(nullable: false),
                    T2 = table.Column<double>(nullable: false),
                    U1 = table.Column<double>(nullable: false),
                    U2 = table.Column<double>(nullable: false),
                    U3 = table.Column<double>(nullable: false),
                    WireConsumptionUpDown = table.Column<double>(nullable: false),
                    WireConsumptionNewInput = table.Column<double>(nullable: false),
                    KDEType = table.Column<string>(nullable: true),
                    KDESeal = table.Column<string>(nullable: true),
                    SwitchSeal = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MounterReportUgesALItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MounterReportUgesALItems_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MounterReportUgesALItems_KDEs_KDEId",
                        column: x => x.KDEId,
                        principalTable: "KDEs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MounterReportUgesALItems_Workers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "Workers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentRegPoints_RegPointId",
                table: "CommentRegPoints",
                column: "RegPointId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentSubstations_SubstationId",
                table: "CommentSubstations",
                column: "SubstationId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryActs_ContractId",
                table: "DeliveryActs",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryActs_DeliveryStateId",
                table: "DeliveryActs",
                column: "DeliveryStateId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryActs_DeliveryTypeId",
                table: "DeliveryActs",
                column: "DeliveryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryActs_UserId",
                table: "DeliveryActs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryActs_WorkerId",
                table: "DeliveryActs",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDeliveries_DeliveryActId",
                table: "DeviceDeliveries",
                column: "DeliveryActId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDeliveries_DeviceId",
                table: "DeviceDeliveries",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceDeliveries_DeviceTypeId",
                table: "DeviceDeliveries",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_ContractId",
                table: "Devices",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceTypeId",
                table: "Devices",
                column: "DeviceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceStates_DeviceId",
                table: "DeviceStates",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceStates_DeviceStateTypeId",
                table: "DeviceStates",
                column: "DeviceStateTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceStates_UserId",
                table: "DeviceStates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentContracts_WorkerId",
                table: "EmploymentContracts",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallActs_InstallActTypeId",
                table: "InstallActs",
                column: "InstallActTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_InstallActs_RegPointId",
                table: "InstallActs",
                column: "RegPointId");

            migrationBuilder.CreateIndex(
                name: "IX_KDEs_KDETypeId",
                table: "KDEs",
                column: "KDETypeId");

            migrationBuilder.CreateIndex(
                name: "IX_KDEs_PowerLineSupportId",
                table: "KDEs",
                column: "PowerLineSupportId");

            migrationBuilder.CreateIndex(
                name: "IX_Letters_RegPointId",
                table: "Letters",
                column: "RegPointId");

            migrationBuilder.CreateIndex(
                name: "IX_Links_DeviceId",
                table: "Links",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialDeliveries_DeliveryActId",
                table: "MaterialDeliveries",
                column: "DeliveryActId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialDeliveries_MaterialId",
                table: "MaterialDeliveries",
                column: "MaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialDeliveries_MaterialTypeId",
                table: "MaterialDeliveries",
                column: "MaterialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_ContractId",
                table: "Materials",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_MaterialTypeId",
                table: "Materials",
                column: "MaterialTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialTypes_MaterialUnitId",
                table: "MaterialTypes",
                column: "MaterialUnitId");

            migrationBuilder.CreateIndex(
                name: "IX_MounterReportUgesALItems_DeviceId",
                table: "MounterReportUgesALItems",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_MounterReportUgesALItems_KDEId",
                table: "MounterReportUgesALItems",
                column: "KDEId");

            migrationBuilder.CreateIndex(
                name: "IX_MounterReportUgesALItems_WorkerId",
                table: "MounterReportUgesALItems",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_MounterReportUgesALs_ContractId",
                table: "MounterReportUgesALs",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_MounterReportUgesALs_NetRegionId",
                table: "MounterReportUgesALs",
                column: "NetRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_MounterReportUgesALs_WorkerId",
                table: "MounterReportUgesALs",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_NetRegions_ContractId",
                table: "NetRegions",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerLineSupports_MounterReportUgesALId",
                table: "PowerLineSupports",
                column: "MounterReportUgesALId");

            migrationBuilder.CreateIndex(
                name: "IX_PPEDeliveries_DeliveryActId",
                table: "PPEDeliveries",
                column: "DeliveryActId");

            migrationBuilder.CreateIndex(
                name: "IX_PPEDeliveries_PPEId",
                table: "PPEDeliveries",
                column: "PPEId");

            migrationBuilder.CreateIndex(
                name: "IX_RegPointFlags_RegPointId",
                table: "RegPointFlags",
                column: "RegPointId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegPoints_ConsumerId",
                table: "RegPoints",
                column: "ConsumerId");

            migrationBuilder.CreateIndex(
                name: "IX_RegPoints_DeviceId",
                table: "RegPoints",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_RegPoints_SubstationId",
                table: "RegPoints",
                column: "SubstationId");

            migrationBuilder.CreateIndex(
                name: "IX_Substations_NetRegionId",
                table: "Substations",
                column: "NetRegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Substations_SubstationStateId",
                table: "Substations",
                column: "SubstationStateId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_WorkerTypeId",
                table: "Workers",
                column: "WorkerTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentRegPoints");

            migrationBuilder.DropTable(
                name: "CommentSubstations");

            migrationBuilder.DropTable(
                name: "DeviceDeliveries");

            migrationBuilder.DropTable(
                name: "DeviceStates");

            migrationBuilder.DropTable(
                name: "EmploymentContracts");

            migrationBuilder.DropTable(
                name: "InstallActs");

            migrationBuilder.DropTable(
                name: "Letters");

            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "MaterialDeliveries");

            migrationBuilder.DropTable(
                name: "MounterReportUgesALItems");

            migrationBuilder.DropTable(
                name: "PPEDeliveries");

            migrationBuilder.DropTable(
                name: "RegPointFlags");

            migrationBuilder.DropTable(
                name: "DeviceStateTypes");

            migrationBuilder.DropTable(
                name: "InstallActTypes");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "KDEs");

            migrationBuilder.DropTable(
                name: "DeliveryActs");

            migrationBuilder.DropTable(
                name: "PPEs");

            migrationBuilder.DropTable(
                name: "RegPoints");

            migrationBuilder.DropTable(
                name: "MaterialTypes");

            migrationBuilder.DropTable(
                name: "KDETypes");

            migrationBuilder.DropTable(
                name: "PowerLineSupports");

            migrationBuilder.DropTable(
                name: "DeliveryStates");

            migrationBuilder.DropTable(
                name: "DeliveryTypes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Consumers");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Substations");

            migrationBuilder.DropTable(
                name: "MaterialUnits");

            migrationBuilder.DropTable(
                name: "MounterReportUgesALs");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "DeviceTypes");

            migrationBuilder.DropTable(
                name: "SubstationStates");

            migrationBuilder.DropTable(
                name: "NetRegions");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "WorkerTypes");
        }
    }
}
