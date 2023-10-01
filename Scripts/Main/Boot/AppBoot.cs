using System.Collections;
using DG.Tweening;
using sm_application.Context;
using sm_application.Events;
using sm_application.Service;
using sm_application.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace sm_application.Boot
{
    public class AppBoot : MonoBehaviour
    {
        [SerializeField] private GameContext _gameContext;
        [Space]
        [SerializeField] private string _gameBootScene;
        [SerializeField] private ScreenServiceInstaller _screenServiceInstaller;
        [SerializeField] private ControlServiceInstaller _controlServiceInstaller;
        [SerializeField] private DebugServiceInstaller _debugServiceInstaller;
        [SerializeField] private SettingsServiceInstaller _settingsServiceInstaller;
        [SerializeField] private AudioServiceInstaller _audioServiceInstaller;

        public void Awake()
        {
            AppContext.Instantiate();
            DOTween.SetTweensCapacity(1000, 50);
            Services.Register<HardwareService>();
            Services.Register<ControlService>(_controlServiceInstaller);
            Services.Register<ScreenService>(_screenServiceInstaller);
            Services.Register<PoolService>();
            Services.Register<DebugService>(_debugServiceInstaller);
            Services.Register<AudioService>(_audioServiceInstaller);
            Services.Register<SettingsService>(_settingsServiceInstaller);
            Services.Register<LocalizationService>();
            Services.Register<SceneLoaderService>();
            
            SystemsService.Bind<ScreenSystem>();
            SystemsService.Bind<DebugSystem>();
            SystemsService.Bind<LocalizationSystem>();

            new BootAppInitializedEvent().Fire();
            
            _gameContext.Construct();
            
            SceneManager.LoadScene(_gameBootScene);
        }
    }
}
