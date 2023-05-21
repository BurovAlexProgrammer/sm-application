using sm_application.Scripts.Main.Service;
using UnityEngine;

namespace sm_application.Scripts.Main.Settings
{
    [CreateAssetMenu(menuName = "Custom/Settings/Audio Settings")]
        public class AudioSettings : SettingsSO
        {
            public bool SoundEnabled;
            public float SoundVolume;
            public bool MusicEnabled;
            public float MusicVolume;

            public override void ApplySettings(SettingsService settingsService)
            {
            }
        }
        
        public static class AudioSettingsAttributes
        {
            public static float VolumeMin = 0f;
            public static float VolumeMax = 20f;
        }
}