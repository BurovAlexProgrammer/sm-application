using sm_application.Scripts.Main.Settings;

namespace sm_application.Scripts.Main.Service
{
    public class SettingsServiceInstaller : BaseServiceInstaller
    {
        public SettingGroup<VideoSettings> VideoSettings;
        public SettingGroup<AudioSettings> AudioSettings;
        public SettingGroup<GameSettings> GameSettings;
    }
}