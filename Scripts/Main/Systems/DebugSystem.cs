using Cysharp.Threading.Tasks;
using sm_application.Events;
using sm_application.Events.Audio;
using sm_application.Service;

namespace sm_application.Systems
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
            AddListener<GameContextInitializedEvent>(GameContextInitialized);
        }

        private async void GameContextInitialized(BaseEvent obj)
        {
            await UniTask.NextFrame();
            var audioListener = _audioService.AudioListener;
            _screenService.SetAudioListenerToCamera(audioListener);
            _screenService.SetupInternalProfiler(audioListener);
        }

        public override void RemoveEventHandlers()
        {
            base.RemoveEventHandlers();
            RemoveListener<GameContextInitializedEvent>();
        }
    }
}