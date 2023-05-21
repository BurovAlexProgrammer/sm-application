using System;
using sm_application.Scripts.Main.Service;
using sm_application.Scripts.Main.Service;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace sm_application.Scripts.Main.Settings
{
    [Serializable]
    [CreateAssetMenu(menuName = "Custom/Settings/Video Settings")]
    public class VideoSettings: SettingsSO
    {
        public bool PostProcessAntiAliasing; //TODO impl
        public bool PostProcessBloom;
        public bool PostProcessVignette;
        public bool PostProcessAmbientOcclusion; //TODO impl
        public bool PostProcessDepthOfField;
        public bool PostProcessFilmGrain;
        public bool PostProcessLensDistortion;
        public bool PostProcessMotionBlur;

        private VolumeProfile _volumeProfile;
        private VolumeComponent _volumeComponent;

        public override void ApplySettings(SettingsService settingsService)
        {
            Services.Get<ScreenService>().ApplySettings(this);
        }
    }
}