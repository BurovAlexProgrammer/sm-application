using sm_application.Settings;

namespace sm_application.Service
{
    public class SettingsServiceInstaller : BaseServiceInstaller
    {
        public SettingGroup<VideoSettings> VideoSettings;
        public SettingGroup<AudioSettings> AudioSettings;
        public SettingGroup<GameSettings> GameSettings;
    }
}