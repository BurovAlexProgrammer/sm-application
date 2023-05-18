using smApplication.Scripts.Main.Services;
using UnityEngine;

namespace smApplication.Scripts.Main.Settings
{
    public abstract class SettingsSO : ScriptableObject
    {
        public string Description;

        public abstract void ApplySettings(SettingsService settingsService);
    }
}