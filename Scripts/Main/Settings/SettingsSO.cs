using sm_application.Service;
using UnityEngine;

namespace sm_application.Settings
{
    public abstract class SettingsSO : ScriptableObject
    {
        public string Description;

        public abstract void ApplySettings(SettingsService settingsService);
    }
}