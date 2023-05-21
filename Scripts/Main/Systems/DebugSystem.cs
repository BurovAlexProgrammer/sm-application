using sm_application.Scripts.Main.Events;
using sm_application.Scripts.Main.Events.Audio;
using sm_application.Scripts.Main.Service;
using sm_application.Scripts.Main.Service;

namespace sm_application.Scripts.Main.Systems
{
    public class DebugSystem : BaseSystem
    {
        private ScreenService _screenService;
        private AudioService _audioService;
        
        public override void Init()
        {
            base.Init();
            _screenService = Services.Get<ScreenService>();
            _audioService = Services.Get<AudioService>();
        }

        public override void AddEventHandlers()
        {
            base.AddEventHandlers();
            AddListener<StartupSystemsLateInitEvent>(SystemsLateInitialized);
        }

        private void SystemsLateInitialized(BaseEvent obj)
        {
            var audioListener = _audioService.AudioListener;
            _screenService.SetAudioListenerToCamera(audioListener);
            _screenService.SetupInternalProfiler(audioListener);
        }

        public override void RemoveEventHandlers()
        {
            base.RemoveEventHandlers();
            RemoveListener<StartupSystemsLateInitEvent>();
        }
    }
}