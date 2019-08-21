using System;
using System.Collections.Generic;
using System.Text;

namespace StoreDbManager.QueryModels
{
    public class SubstationRowQueryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StateName { get; set; }
        public string PhoneNumber { get; set; }
        //Статистика по точкам
        public int CountRegPoints { get; set; }
        public int CountUSPD { get; set; }
        public int CountForImportConsumer { get; set; }
        public int CountRegPointsAscueOk { get; set; }
        public int CountRegPointsAscueChecked { get; set; }
        public int CountRegPointsLinkOk { get; set; }
        // Флаги
        public bool IsInstallationDone { get; set; }
        public bool IsPropSchemeDone { get; set; }
        public bool IsBalanceDone { get; set; }
        public DateTime LastChanges { get; set; } = DateTime.MinValue;
        public string LastChangesFormated { get => LastChanges.ToString("dd MMM yyyy HH:mm"); }
    }
}
