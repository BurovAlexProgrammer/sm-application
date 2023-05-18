using smApplication.Scripts.Main.Services;
using smApplication.Scripts.Main.Settings;
using UnityEngine;

namespace smApplication.Scripts.Main.Menu
{
    public class MenuSettingsController : MonoBehaviour
    {
        public void Apply()
        {
            var settingService = Services.Services.Get<SettingsService>();
            settingService.Save();
            settingService.Apply();
        }
        
        public void Save()
        {
            var settingService = Services.Services.Get<SettingsService>();
            settingService.Save();
            settingService.Apply();
        }
        
        public void ResetToDefault()
        {
            var settingService = Services.Services.Get<SettingsService>();
            settingService.Restore();
            settingService.Apply();
        }

        public void Bind(bool value, ref bool settingsValue)
        {
            settingsValue = value;
        }
    }
}