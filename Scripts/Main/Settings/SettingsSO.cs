using sm_application.Scripts.Main.Service;
using UnityEngine;

namespace sm_application.Scripts.Main.Settings
{
    public abstract class SettingsSO : ScriptableObject
    {
        public string Description;

        public abstract void ApplySettings(SettingsService settingsService);
    }
}