using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbManager
{
    public class MaterialManager
    {
        StoreContext db;
        public MaterialManager()
        {
           db = new StoreContext();
        }

        public bool NewMaterialType(string typeName, int unitId, double price)
        {
            MaterialUnit unit = db.MaterialUnits.Find(unitId);
            MaterialType type = db.MaterialTypes.FirstOrDefault(t => t.Name == typeName);
            if (unit != null)
            {
                if (type == null)
                {
                    db.MaterialTypes.Add(new MaterialType { Name = typeName, MaterialUnitId = unit.Id, Price =price });
                    db.SaveChanges();
                    return true;
                }
                else return false;
            }
            else return false;
        }

        public bool DeleteMaterialType (int materialTypeId)
        {
            MaterialType type = db.MaterialTypes.Find(materialTypeId);
            if (type != null)
            {
                try
                {
                    db.MaterialTypes.Remove(type);
                    db.SaveChanges();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else return false;
        }
    }
}
