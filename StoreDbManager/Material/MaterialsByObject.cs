using DbManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StoreDbManager
{
    public static class MaterialsByObject
    {
        /// <summary>
        /// Получить кол-во счетчиков по моделям на подстанции
        /// </summary>
        /// <param name="substationId"> id подстанции</param>
        /// <returns>Сгрупиррованый список</returns>
        public static List<IGrouping<string, Device>> GetDeiceTypes(int substationId)
        {
            using (StoreContext _db = new StoreContext())
            {
                var points = from r in _db.RegPoints
                             where r.SubstationId == substationId && r.Status == RegPointStatus.Default
                             from d in _db.Devices
                             where r.DeviceId == d.Id
                             select d;
                return points.GroupBy(p => p.DeviceType.Name).ToList();
            }
        }

        /// <summary>
        /// Получить доп оборудование привязанное к подстанции
        /// </summary>
        /// <param name="substationId"></param>
        /// <returns></returns>
        public static List<IGrouping<string, Device>> GetSubstationDevices(int substationId)
        {
            using (StoreContext _db = new StoreContext())
            {
                var points = from r in _db.SubstationDevices
                             where r.SubstationId == substationId
                             from d in _db.Devices
                             where r.DeviceId == d.Id
                             select d;
                return points.GroupBy(p => p.DeviceType.Name).ToList();
            }
        }

        /// <summary>
        /// Получить кол-во кде на постанции по типам по отчетам монтажников
        /// </summary>
        /// <param name="substationId">id подстанции</param>
        public static List<IGrouping<string, KDE>> GetMountersKdeTypes(int substationId)
        {
            using (StoreContext _db = new StoreContext())
            {
                /*var points2 = from point in _db.RegPoints
                              join reportItem in _db.MounterReportUgesALItems
                              on point.DeviceId equals reportItem.DeviceId
                              join flags in _db.RegPointFlags
                              on point.Id equals flags.RegPointId
                              join kde in _db.KDEs
                              on reportItem.KDEId equals kde.Id
                              join Type in _db.KDETypes
                              on kde.KDETypeId equals Type.Id
                              where point.SubstationId == substationId && point.Status == RegPointStatus.Default && !flags.IsReplace
                              group reportItem by reportItem.Serial into groups
                              select groups;*/
                //var group = points2.ToList();

                var points = from point in _db.RegPoints
                             join reportItem in _db.MounterReportUgesALItems
                             on point.DeviceId equals reportItem.DeviceId
                             join flags in _db.RegPointFlags
                             on point.Id equals flags.RegPointId
                             where point.SubstationId == substationId && point.Status == RegPointStatus.Default && !flags.IsReplace
                             select reportItem.KDE;
                
                return points.GroupBy(p => p.KDEType.Name).ToList();
               
            }
        }

        /// <summary>
        /// Получить кол-во кде на постанции по типам по привзке на складе
        /// </summary>
        /// <param name="substationId"></param>
        /// <returns></returns>
        public static KdeTypesStore GetKdeTypesStore(int substationId)
        {
            using (StoreContext _db = new StoreContext())
            {
                var result = new KdeTypesStore();
                var points = from point in _db.RegPoints
                             where point.SubstationId == substationId && point.Status == RegPointStatus.Default
                             from d in _db.Devices
                             where point.DeviceId == d.Id
                             select new
                             {
                                 d,
                                 d.DeviceType,
                                 d.KDE,
                                 d.KDE.KDEType
                             };

                foreach (var item in points)
                {
                    if (item.KDE != null)
                    {
                        if (item.KDEType.Name == "КДЕ-3-2")
                            result.kde32Count++;
                        if (item.KDEType.Name == "КДЕ-3-1")
                            result.Kde3ount++;
                    }
                    else
                    {
                        if (item.d.DeviceType.Description.ToLower().Contains("1ф"))
                            result.Kde1Count++;
                        if (item.d.DeviceType.Description.ToLower().Contains("3ф"))
                            result.Kde3ount++;
                    }
                }
                result.Kde3ount += result.kde32Count / 2;
                return result;
            }
        }

        /// <summary>
        /// Получение списка использованных материалов на ТП
        /// </summary>
        /// <param name="substationId">id подстанции</param>
        /// <returns></returns>
        public static List<SubstationMaterial> GetMaterials(int substationId)
        {
            using (StoreContext _db = new StoreContext())
            {
                bool materialFound = false;
                var result = new List<SubstationMaterial>();

                //Учитываем статус ТУ при подсчете по формулам
                var points = from point in _db.RegPoints
                             where point.SubstationId == substationId && point.Status == RegPointStatus.Default
                             from flags in _db.RegPointFlags
                             where flags.RegPointId == point.Id
                             from device in _db.Devices
                             where device.Id == point.DeviceId
                             from reportItem in _db.MounterReportUgesALItems
                             where reportItem.DeviceId == device.Id
                             select new
                             {
                                 device,
                                 device.DeviceType,
                                 device.AdditionalMaterials,
                                 reportItem,
                                 reportItem.KDE,
                                 reportItem.KDE.KDEType,
                                 reportItem.KDE.PowerLineSupport,
                                 flags.IsReplace
                             };
                var devices = points.ToList();

                //Не учитываем статус ТУ при подсчете по отчетам монтажников
                var Regpoint = from Regpoints in _db.RegPoints
                               where Regpoints.SubstationId == substationId
                               from device in _db.Devices
                               where device.Id == Regpoints.DeviceId
                               from reportItem in _db.MounterReportUgesALItems
                               where reportItem.DeviceId == device.Id
                               select new
                               {
                                   device,
                                   device.DeviceType,
                                   device.AdditionalMaterials,
                                   reportItem,
                                   reportItem.KDE,
                                   reportItem.KDE.KDEType,
                                   reportItem.KDE.PowerLineSupport
                               };
                var Regdevices = Regpoint.ToList();

                //Считаем количество фиксаторов
                var Fixators = from Regpoints in _db.RegPoints
                               where Regpoints.SubstationId == substationId
                               from device in _db.Devices
                               where device.Id == Regpoints.DeviceId
                               from reportItem in _db.MounterReportUgesALItems
                               where reportItem.DeviceId == device.Id
                               from kde in _db.KDEs
                               where kde.Id == reportItem.KDEId 
                               from powerline in _db.PowerLineSupports
                               where powerline.Id == kde.PowerLineSupportId
                               group powerline by new
                               {
                                   Id = powerline.Id,
                                   supportnumber = powerline.SupportNumber,
                                   powerlinetype = powerline.PowerLineType,
                                   fixatorcount = powerline.FixatorsCount,
                                   reportsitem = powerline.MounterReportUgesALId
                               } into g
                               select g;
                var FixatorCount = Fixators.ToList();

                var sip2 = _db.MaterialTypes.FirstOrDefault(m => m.Name.Contains("СИП-4 2х16"));
                var sip4 = _db.MaterialTypes.FirstOrDefault(m => m.Name.Contains("СИП-4 4х16"));
                double sip2Volume = 0;
                double sip4Volume = 0;
                int sealBECount = 0;
                //подсчитываемые материалы
                double fixatorsCount = 0; //Дистанционный фиксатор
                double metalTape = 0; //Лента металлическая
                double anchorBracket = 0; //Кронштейн анкерный
                double bracket = 0; //Скрепа
                double anchorWedgeClamp = 0; //Анкерный клиновой зажим
                double piercingClamp = 0; //Прокалывающий зажим Сип-Сип
                double piercingClampGP = 0; //Прокалывающий зажим Сип-Голый провод
                double bandStrap = 0; //Ремешок-хамут стяжной
                double sealBE = 0; //Пломба контрольная БЭ
                double wire = 0; //Проволока витая металлическая
                double redSticker = 0; //Пломба стикер-красная
                double blueSeal = 0; //синяя пломба
                double wirePV = 0; //провод ПУВ 1x10

                int kde1 = 0;
                int kde1Replace = 0;
                int kde32 = 0;
                int kde32Replace = 0;
                int kde3 = 0;
                int kde3Replace = 0;


                //Учитываем статус ТУ при подсчете по формулам
                foreach (var item in devices)
                {
                    if (item.IsReplace == false)
                    {
                        //Расчет комплекта крепежа
                        if (item.DeviceType.Description.ToLower().Contains("1ф")) //Если ПУ однофазный
                        {
                            if (item.reportItem.KDE.KDEType.Name != "КДЕ-3-2") //Если ПУ не в КДЕ 3-2
                            {
                                piercingClamp += 2;
                                if (item.PowerLineSupport.PowerLineType == item.reportItem.PowerLineType)
                                    piercingClamp += 2;
                                else piercingClampGP += 2;
                                bandStrap += 2;
                                wire += 0.55;
                                sealBE += 2;
                                redSticker += 2;
                                wirePV += 1.3;
                                //fixatorsCount += 3;

                                if (item.reportItem.WireConsumptionNewInput == 0) //Без замены абонентского СИП
                                {
                                    metalTape += 5;
                                    bracket += 5;
                                    kde1++;
                                }
                                else
                                {
                                    metalTape += 6;
                                    anchorBracket += 2;
                                    bracket += 6;
                                    anchorWedgeClamp += 2;
                                    kde1Replace++;

                                }
                            }
                            else  //Если ПУ в КДЕ 3-2
                            {
                                piercingClamp += 4 / 2.0;
                                if (item.PowerLineSupport.PowerLineType == item.reportItem.PowerLineType)
                                    piercingClamp += 4 / 2.0;
                                else piercingClampGP += 4 / 2.0;
                                bandStrap += 4 / 2.0;
                                wire += 1 / 2.0;
                                sealBE += 3 / 2.0;
                                redSticker += 4 / 2.0;
                                wirePV += 2.9 / 2.0;
                                //fixatorsCount += 5 / 2.0;
                                if (item.reportItem.WireConsumptionNewInput == 0) //Без замены абонентского СИП
                                {
                                    metalTape += 7 / 2.0;
                                    bracket += 7 / 2.0;
                                    kde32++;
                                }
                                else
                                {
                                    metalTape += 8 / 2.0;
                                    anchorBracket += 4 / 2.0;
                                    bracket += 8 / 2.0;
                                    anchorWedgeClamp += 4 / 2.0;
                                    kde32Replace++;
                                }
                            }
                        }

                        if (item.DeviceType.Description.ToLower().Contains("3ф") && (item.DeviceType.Description.ToLower().Contains("gsm") || item.DeviceType.Description.ToLower().Contains("plc"))) //Если ПУ 3ф
                        {
                            redSticker += 3;
                            wirePV += 1.8;
                            //fixatorsCount += 3;
                            wire += 0.6;
                            sealBE += 3;
                            bandStrap += 2;//4
                            piercingClamp += 4;
                            if (item.PowerLineSupport.PowerLineType == item.reportItem.PowerLineType)
                                piercingClamp += 4;
                            else piercingClampGP += 4;

                            if (item.reportItem.WireConsumptionNewInput == 0) //Без замены абонентского СИП
                            {
                                metalTape += 5;
                                bracket += 5;
                                kde3++;
                            }
                            else
                            {
                                metalTape += 6;
                                anchorBracket += 2;
                                bracket += 6;
                                anchorWedgeClamp += 2;
                                kde3Replace++;

                            }
                        }
                    }
                    else
                    {
                        if (item.DeviceType.Description.ToLower().Contains("1ф")) //Если ПУ однофазный
                        {
                            if (item.reportItem.KDE.KDEType.Name != "КДЕ-3-2") //Если ПУ не в КДЕ 3-2
                            {
                                wire += 0.2;
                            }
                            else  //Если ПУ в КДЕ 3-2
                            {
                                wire += 0.6 / 2.0;                                
                            }
                        }

                        if (item.DeviceType.Description.ToLower().Contains("3ф") && (item.DeviceType.Description.ToLower().Contains("gsm") || item.DeviceType.Description.ToLower().Contains("plc"))) //Если ПУ 3ф
                        {
                            wire += 0.2;                            
                        }
                    }                                        
                }

                //Не учитываем статус ТУ при подсчете по данным из БД
                foreach (var item in Regdevices)
                {
                    if (item.PowerLineSupport.SupportNumber == 0)
                        fixatorsCount += item.PowerLineSupport.FixatorsCount;

                    if (item.DeviceType.Description.ToLower().Contains("1ф")) //Подсчет использованного СИПа для 1ф ПУ
                        sip2Volume += item.reportItem.WireConsumptionNewInput + item.reportItem.WireConsumptionUpDown;

                    if (item.DeviceType.Description.ToLower().Contains("3ф")) //Подсчет использованного СИПа для 3ф ПУ
                        sip4Volume += item.reportItem.WireConsumptionNewInput + item.reportItem.WireConsumptionUpDown;


                    //Если к ПУ прикреплен комплект, считаются только Пломбы и проволока по просьбе Сметчиков 12.04.19
                    if (item.device.SetId != 0 && item.device.SetId != null)
                    {
                        DeliveryAct set = _db.DeliveryActs.Find(item.device.SetId);
                        if (set != null)
                        {
                            foreach (var material in set.MaterialDeliveries)
                            {
                                if (material.MaterialType.Name.ToLower().Contains("пломба") || material.MaterialType.Name.ToLower().Contains("проволока"))
                                {
                                    if (material.MaterialType.Name.ToLower().Contains("башкирэнерго"))
                                        sealBECount++;
                                    if (material.MaterialType.Name.ToLower().Contains("пломба") && !material.MaterialType.Name.ToLower().Contains("башкирэнерго"))
                                        blueSeal++;
                                }
                            }
                        }
                    }

                    //fixatorsCount += item.reportItem.KDE.PowerLineSupport.FixatorsCount;

                    //Доп материалы указанные монтажником
                    materialFound = false;
                    foreach (var addMaterial in item.AdditionalMaterials)
                    {
                        var material = _db.Materials.Find(addMaterial.MaterialId);
                        var materialType = _db.MaterialTypes.Find(material.MaterialTypeId);
                        if (materialType != null)
                        {
                            foreach (var subMaterial in result)
                            {
                                if (materialType.Name == subMaterial.Name)
                                {
                                    materialFound = true;
                                    subMaterial.Volume += addMaterial.Volume;
                                }
                            }
                            if (!materialFound)
                            {
                                result.Add(new SubstationMaterial { Name = materialType.Name, Volume = addMaterial.Volume, Unit = materialType.Unit.Name });
                            }
                            materialFound = false;
                        }
                    }
                }

                if (fixatorsCount == 0)
                {
                    foreach (var fixators in FixatorCount)
                    {
                        if (fixators.Key.supportnumber != 0)
                            fixatorsCount += fixators.Key.fixatorcount;
                    }
                }                         

                result.Add(new SubstationMaterial { Name = "Провод СИП-4 2х16", Volume = sip2Volume, Unit = "м" });
                result.Add(new SubstationMaterial { Name = "Провод СИП-4 4х16", Volume = sip4Volume, Unit = "м" });
                result.Add(new SubstationMaterial { Name = "Дистанционный фиксатор", Volume = fixatorsCount, Unit = "шт" });
                result.Add(new SubstationMaterial { Name = "Лента металлическая", Volume = metalTape, Unit = "м" }); //Лента металлическая
                result.Add(new SubstationMaterial { Name = "Кронштейн анкерный", Volume = anchorBracket, Unit = "шт" }); //Кронштейн анкерный
                result.Add(new SubstationMaterial { Name = "Скрепа", Volume = bracket, Unit = "шт" }); //Скрепа
                result.Add(new SubstationMaterial { Name = "Анкерный клиновой зажим", Volume = anchorWedgeClamp, Unit = "шт" }); //Анкерный клиновой зажим
                result.Add(new SubstationMaterial { Name = "Прокалывающий зажим СИП-СИП", Volume = piercingClamp, Unit = "шт" }); //Прокалывающий зажим Сип-Сип
                result.Add(new SubstationMaterial { Name = "Прокалывающий зажим СИП-Голый Провод", Volume = piercingClampGP, Unit = "шт" }); //Прокалывающий зажим Сип-Голый провод
                result.Add(new SubstationMaterial { Name = "Ремешок-хамут стяжной", Volume = bandStrap, Unit = "шт" }); //Ремешок-хамут стяжной
                result.Add(new SubstationMaterial { Name = "Пломба Эксперт (оранжевый,лм, ООО Башкирэнерго)", Volume = sealBE + sealBECount, Unit = "шт" }); //Пломба контрольная БЭ
                result.Add(new SubstationMaterial { Name = "Проволока витая металлическая", Volume = wire, Unit = "м" }); //Проволока витая металлическая
                result.Add(new SubstationMaterial { Name = "Пломба-стикер красная", Volume = redSticker, Unit = "шт" });//Пломба стикер-красная
                result.Add(new SubstationMaterial { Name = "Пломба синяя", Volume = blueSeal, Unit = "шт" });
                result.Add(new SubstationMaterial { Name = "Провод ПВ 1x10", Volume = wirePV, Unit = "м" }); //Провод ПВ-1 1x10
                result.AddRange(GetSetMaterials(substationId).OrderBy(x => x.Name));
                result.Add(new SubstationMaterial { Name = "КДЕ 1", Volume = kde1 });
                result.Add(new SubstationMaterial { Name = "КДЕ 1 c заменой", Volume = kde1Replace });
                result.Add(new SubstationMaterial { Name = "КДЕ 32", Volume = kde32 / 2 });
                result.Add(new SubstationMaterial { Name = "КДЕ 32 с заменой", Volume = kde32Replace / 2 });
                result.Add(new SubstationMaterial { Name = "КДЕ 3", Volume = kde3 });
                result.Add(new SubstationMaterial { Name = "КДЕ 3 с заменой", Volume = kde3Replace });
                return result;
            }
        }

        /// <summary>
        /// Получить список комплектов на подстанции 
        /// </summary>
        /// <param name="substationId">Id подстанции</param>
        /// <returns></returns>
        public static List<SubstationMaterial> GetSubstationSets(int substationId)
        {
            using (StoreContext _db = new StoreContext())
            {
                bool materialFound = false;
                var result = new List<SubstationMaterial>();

                var points = from point in _db.RegPoints
                             where point.SubstationId == substationId && (point.Status == RegPointStatus.Default || point.Status == RegPointStatus.Demounted)
                             from device in _db.Devices
                             where device.Id == point.DeviceId && device.DeviceTypeId != 6
                             from flags in _db.RegPointFlags
                             where flags.RegPointId == point.Id
                             from type in _db.DeviceTypes
                             where type.Id == device.DeviceTypeId
                             select new
                             {
                                 device,
                                 device.SetId,
                                 flags.IsReplace,
                                 type.Description
                             };

                var devices = points.ToList();
                int replaceSetsCount = 0;
                int nullSetsCount_1 = 0;
                int nullSetsCount_3 = 0;
                int replace_1 = 0;
                int replace_3 = 0;
                foreach (var item in devices)
                {
                    if (item.SetId != 0 && item.SetId != null)
                    {
                        DeliveryAct set = _db.DeliveryActs.Find(item.SetId);
                        if (set != null)
                        {
                            foreach (var setResult in result)
                            {
                                if (setResult.Name == set.SetName)
                                {
                                    materialFound = true;
                                    setResult.Volume++;
                                }
                            }
                            if (!materialFound)
                            {
                                result.Add(new SubstationMaterial { Name = set.SetName, Volume = 1 });
                            }
                            materialFound = false;
                        }
                        else
                        {
                            if (!item.IsReplace && (item.Description == "1ф PLC" || item.Description == "1ф GSM"))
                                nullSetsCount_1++;
                            else if (!item.IsReplace && (item.Description == "3ф PLC" || item.Description == "3ф GSM"))
                                nullSetsCount_3++;
                        }
                    }
                    else
                        if (item.IsReplace && (item.Description == "1ф PLC" || item.Description == "1ф GSM"))
                            replace_1++;
                        else if (item.IsReplace && (item.Description == "3ф PLC" || item.Description == "3ф GSM"))
                            replace_3++;
                        else
                        replaceSetsCount++;
                }
                var kdes32 = result.Where(r => r.Name.ToLower().Contains("кде-3-2"));
                foreach (var item in kdes32)
                {
                    item.Volume /= 2;
                }
                result.Add(new SubstationMaterial { Name = "ПУ без комплектов", Volume = replaceSetsCount });
                result.Add(new SubstationMaterial { Name = "1ф ПУ для которых не найден комплект", Volume = nullSetsCount_1 });
                result.Add(new SubstationMaterial { Name = "3ф ПУ для которых не найден комплект", Volume = nullSetsCount_3 });
                result.Add(new SubstationMaterial { Name = "Замена 1ф ПУ 2017 года", Volume = replace_1 });
                result.Add(new SubstationMaterial { Name = "Замена 3ф ПУ 2017 года", Volume = replace_3 });
                return result;
            }
        }

        /// <summary>
        /// Получить список материалов из комплектов
        /// </summary>
        /// <param name="substationId"></param>
        /// <returns></returns>
        public static List<SubstationMaterial> GetSetMaterials(int substationId)
        {
            bool materialFound = false;
            var result = new List<SubstationMaterial>();
            using (StoreContext _db = new StoreContext())
            {

                var points = from point in _db.RegPoints
                             where point.SubstationId == substationId && (point.Status == RegPointStatus.Default || point.Status == RegPointStatus.Demounted)
                             from device in _db.Devices
                             where device.Id == point.DeviceId
                             from reportItem in _db.MounterReportUgesALItems
                             where reportItem.DeviceId == device.Id
                             select new
                             {
                                 device,
                                 device.DeviceType,
                                 device.AdditionalMaterials,
                                 reportItem,
                                 reportItem.KDE,
                                 reportItem.KDE.KDEType,
                                 reportItem.KDE.PowerLineSupport
                             };
                var devices = points.ToList();


                foreach (var item in devices)
                {

                    //Если к ПУ прикреплен комплект, считаются только Пломбы и проволока по просьбе Сметчиков 12.04.19
                    if (item.device.SetId != 0 && item.device.SetId != null)
                    {
                        DeliveryAct set = _db.DeliveryActs.Find(item.device.SetId);
                        if (set != null)
                        {
                            foreach (var material in set.MaterialDeliveries)
                            {
                                if (!material.MaterialType.Name.ToLower().Contains("пломба") && !material.MaterialType.Name.ToLower().Contains("проволока") && !material.MaterialType.Name.ToLower().Contains("шкаф учёта") && !material.MaterialType.Name.ToLower().Contains("провод"))
                                {
                                    foreach (var subMaterial in result)
                                    {
                                        if (material.MaterialType.Name == subMaterial.Name)
                                        {
                                            materialFound = true;
                                            subMaterial.Volume += material.Volume;
                                        }
                                    }
                                    if (!materialFound)
                                    {
                                        result.Add(new SubstationMaterial { Name = material.MaterialType.Name, Volume = material.Volume, Unit = material.MaterialType.Unit.Name });
                                    }
                                    materialFound = false;
                                }
                            }
                        }
                    }
                }
                return result;
            }
        }

        public static List<SubstationMaterial> GetSBMaterials (int substationId)
        {
            var result = new List<SubstationMaterial>();
            
            using (StoreContext _db = new StoreContext())
            {
                var substation = _db.Substations.Find(substationId);
                if (substation != null)
                {
                    var report = _db.USPDReports.FirstOrDefault(r => r.ContractId == substation.NetRegion.ContractId && r.Substation == substation.Name);
                    if (report != null)
                    {
                        result.Add(new SubstationMaterial() { Name = "Труба гофр. ПВХ", Volume = report.Gofr, Unit = "м" });
                        result.Add(new SubstationMaterial() { Name = "Кабель FTP", Volume = report.Ftp, Unit = "м" });
                        result.Add(new SubstationMaterial() { Name = "Кабель ВВГ 3x2.5", Volume = report.Kvvg, Unit = "м" });
                    }


                    var _switch = from point in _db.RegPoints
                                   where point.SubstationId == substationId && point.Status == RegPointStatus.Default
                                   from device in _db.Devices
                                   where device.Id == point.DeviceId
                                   from reportItem in _db.Switches
                                   where reportItem.DeviceSerial == device.SerialNumber
                                   select reportItem;

                    var switches = _switch.ToList();
                    double kvvg = 0;
                    foreach (var item in switches)
                    {
                        kvvg += item.KVVG;
                    }
                    result.Add(new SubstationMaterial() { Name = "Кабель КВВГ 10x2.5", Volume = kvvg, Unit = "м" });
                }
            }
            return result;
        }

        /// <summary>
        /// Получить коэффициенты трансформации тока по фазам
        public static List<SubstationMaterial> GetSBTT (int substationId)
        {
            var result = new List<SubstationMaterial>();

            using (StoreContext _db = new StoreContext())
            {
                var _switch = from point in _db.RegPoints
                              where point.SubstationId == substationId && point.Status == RegPointStatus.Default
                              from device in _db.Devices
                              where device.Id == point.DeviceId
                              from switchesTT in _db.Switches
                              where switchesTT.DeviceSerial == device.SerialNumber
                              select new
                              {
                                  switchesTT.TTAk,
                                  switchesTT.TTBk,
                                  switchesTT.TTCk
                              };

                var switches = _switch.ToList();
                string TTAk = "";
                string TTBk = "";
                string TTCk = "";
                foreach (var item in switches)
                {
                    if (TTAk == "")
                    {
                        TTAk = item.TTAk.ToString() + "/5";
                        TTBk = item.TTBk.ToString() + "/5";
                        TTCk = item.TTCk.ToString() + "/5";
                    }
                    else
                    {
                        TTAk = TTAk + ", " + item.TTAk.ToString() + "/5";
                        TTBk = TTBk + ", " + item.TTBk.ToString() + "/5";
                        TTCk = TTCk + ", " + item.TTCk.ToString() + "/5";
                    }
                }
                result.Add(new SubstationMaterial() { Name = "ТТ ф. A", Unit = TTAk });
                result.Add(new SubstationMaterial() { Name = "ТТ ф. B", Unit = TTBk });
                result.Add(new SubstationMaterial() { Name = "ТТ ф. C", Unit = TTCk });
            }
            return result;
        }
    }         
        public class KdeTypesStore
        {
            public int Kde1Count { get; set; }
            public int Kde3ount { get; set; }
            public int kde32Count { get; set; }
            public KdeTypesStore()
            {
                Kde1Count = 0;
                Kde3ount = 0;
                kde32Count = 0;
            }
        }

        public class SubstationMaterial
        {
            public string Name { get; set; }
            public string Unit { get; set; }
            public double Volume { get; set; }
        }
}
