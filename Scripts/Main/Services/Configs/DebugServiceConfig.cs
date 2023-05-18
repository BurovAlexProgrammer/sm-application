using UnityEngine;

namespace smApplication.Scripts.Main.Services
{
    [CreateAssetMenu(menuName = "Custom/Debug Config")]
    public class DebugServiceConfig : ScriptableObject
    {
        public bool SaveLogToFile;
    }
}