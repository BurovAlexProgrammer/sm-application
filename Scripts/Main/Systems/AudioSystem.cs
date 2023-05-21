using sm_application.Scripts.Main.Events;
using sm_application.Scripts.Main.Events.Audio;
using sm_application.Scripts.Main.Service;
using sm_application.Scripts.Main.Service;

namespace sm_application.Scripts.Main.Systems
{
    public class AudioSystem : BaseSystem
    {
        private AudioService _audioService;
        public override void Init()
        {
            base.Init();
            _audioService = Services.Get<AudioService>();
            new AudioSystemInitializedEvent().Fire();
        }

        public override void AddEventHandlers()
        {
            base.AddEventHandlers();
            AddListener<PlayMenuMusicEvent>(PlayMenuMusic);
            AddListener<PlayGameEvent>(OnPlayGame);
            AddListener<ShowMainMenuEvent>(OnShowMainMenu);
        }

        private void OnShowMainMenu(BaseEvent obj)
        { 
            _audioService.PlayMusic(AudioService.MusicPlayerState.MainMenu);
        }

        private void OnPlayGame(BaseEvent obj)
        {
            _audioService.PlayMusic(AudioService.MusicPlayerState.Battle);
        }

        private void PlayMenuMusic(BaseEvent obj)
        {
            _audioService.PlayMusic(AudioService.MusicPlayerState.MainMenu);
        }
    }
}