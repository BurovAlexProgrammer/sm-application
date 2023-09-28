using UnityEngine;

namespace sm_application.Service
{
    public class HardwareService : IService
    {
        public string UniqueDeviceId => SystemInfo.deviceUniqueIdentifier;
        
        public void Construct()
        {
        }
    }
}