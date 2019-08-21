using System;
using System.Collections.Generic;
using System.Text;

namespace DbManager
{
    class DeviceManager
    {
        public DeviceManager()
        {
            StoreContext db = new StoreContext();
        }
        public bool AddNewDevice(string serialNumber)
        {
            
        
            return true;
        }

        public bool ChangeDeviceState (string serialNumber, string newState)
        {
            return true;
        }
    }
}
