using UnityEngine;

namespace sm_application.Scripts.Main.Service
{
    [CreateAssetMenu(menuName = "Custom/Debug Config")]
    public class DebugServiceConfig : ScriptableObject
    {
        public bool SaveLogToFile;
    }
}