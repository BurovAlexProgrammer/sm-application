using System;
using System.Collections;
using DG.Tweening;
using sm_application.Events;
using sm_application.Service;
using sm_application.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;
using AppContext = sm_application.Game.AppContext;

namespace sm_application.Startup
{
    public class AppContextInstaller : MonoBehaviour
    {
        [SerializeField] private string _startupGameScene;
        [SerializeField] private ScreenServiceInstaller _screenServiceInstaller;
        [SerializeField] private ControlServiceInstaller _controlServiceInstaller;
        [SerializeField] private DebugServiceInstaller _debugServiceInstaller;
        [SerializeField] private SettingsServiceInstaller _settingsServiceInstaller;
        [SerializeField] private AudioServiceInstaller _audioServiceInstaller;

        public void Awake()
        {
            Game.AppContext.Instantiate();
            DOTween.SetTweensCapacity(1000, 50);
            Services.Register<HardwareService>();
            Services.Register<ControlService>(_controlServiceInstaller);
            Services.Register<ScreenService>(_screenServiceInstaller);
            Services.Register<PoolService>();
            Services.Register<DebugService>(_debugServiceInstaller);
            Services.Register<AudioService>(_audioServiceInstaller);
            Services.Register<SettingsService>(_settingsServiceInstaller);
            Services.Register<LocalizationService>();
            
            SystemsService.Bind<ControlSystem>();
            SystemsService.Bind<ScreenSystem>();
            SystemsService.Bind<DebugSystem>();
            SystemsService.Bind<AudioSystem>();

            new StartupSystemsInitializedEvent().Fire();
            StartCoroutine(LateStartup());
        }

        private IEnumerator LateStartup()
        {
            yield return null;
            new StartupSystemsLateInitEvent().Fire();
            SceneManager.LoadScene(_startupGameScene);
        }
    }
}
