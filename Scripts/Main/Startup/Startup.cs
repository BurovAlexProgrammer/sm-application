using System.Collections;
using smApplication.Scripts.Main.DTO;
using smApplication.Scripts.Main.Events;
using smApplication.Scripts.Main.Game;
using smApplication.Scripts.Main.Services;
using smApplication.Scripts.Main.Systems;
using DG.Tweening;
using UnityEngine;

namespace smApplication.Scripts.Main.Installers
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private ScreenServiceInstaller _screenServiceInstaller;
        [SerializeField] private ControlServiceInstaller _controlServiceInstaller;
        [SerializeField] private DebugServiceInstaller _debugServiceInstaller;
        [SerializeField] private SettingsServiceInstaller _settingsServiceInstaller;
        [SerializeField] private AudioServiceInstaller _audioServiceInstaller;

        public void Awake() 
        {
            Debug.Log("Startup");

            AppContext.Instantiate();
            DOTween.SetTweensCapacity(1000, 50);
            Services.Services.Register<ControlService>(_controlServiceInstaller);
            Services.Services.Register<ScreenService>(_screenServiceInstaller);
            Services.Services.Register<PoolService>();
            Services.Services.Register<DebugService>(_debugServiceInstaller);
            Services.Services.Register<SceneLoaderService>();
            Services.Services.Register<StatisticService>();
            Services.Services.Register<AudioService>(_audioServiceInstaller);
            Services.Services.Register<SettingsService>(_settingsServiceInstaller);
            Services.Services.Register<GameStateService>();
            Services.Services.Register<LocalizationService>();
            
            SystemsService.Bind<ControlSystem>();
            SystemsService.Bind<ScreenSystem>();
            SystemsService.Bind<SceneLoaderSystem>();
            SystemsService.Bind<DebugSystem>();
            SystemsService.Bind<AudioSystem>();
            SystemsService.Bind<GameStateSystem>();
            
            //Services.Get<StatisticService>().AddValueToRecord(StatisticData.RecordName.Movement, 10f);
            new StartupSystemsInitializedEvent().Fire();
            StartCoroutine(LateStartup());
        }

        private void OnApplicationQuit()
        {
            Services.Services.Dispose();
            SystemsService.Dispose();
        }
        
        private IEnumerator LateStartup()
        {
            yield return null;
            new StartupSystemsLateInitEvent().Fire();
        }
    }
}