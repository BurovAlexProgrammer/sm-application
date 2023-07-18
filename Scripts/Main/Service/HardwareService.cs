using UnityEngine;

namespace sm_application.Scripts.Main.Service
{
    public class HardwareService : IService
    {
        public string UniqueDeviceId => SystemInfo.deviceUniqueIdentifier;
    }
}