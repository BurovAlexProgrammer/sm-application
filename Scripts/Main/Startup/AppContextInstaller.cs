using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Duck.Http;
using Duck.Http.Service.Unity;
using sm_application.Scripts.Main.Events;
using sm_application.Scripts.Main.Service;
using sm_application.Scripts.Main.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;
using AppContext = sm_application.Scripts.Main.Game.AppContext;

namespace sm_application.Scripts.Main.Installers
{
    public class AppContextInstaller : MonoBehaviour
    {
        [SerializeField] private string _startupGameScene;
        [SerializeField] private ScreenServiceInstaller _screenServiceInstaller;
        [SerializeField] private ControlServiceInstaller _controlServiceInstaller;
        [SerializeField] private DebugServiceInstaller _debugServiceInstaller;
        [SerializeField] private SettingsServiceInstaller _settingsServiceInstaller;
        [SerializeField] private AudioServiceInstaller _audioServiceInstaller;

        public event Action OnApplicationInitialized;

        public event Func<string, string> StrFunc;
        
        
        public async void Awake()
        {
            AppContext.Instantiate();
            DOTween.SetTweensCapacity(1000, 50);
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
