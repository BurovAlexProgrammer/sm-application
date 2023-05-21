using System.Collections;
using DG.Tweening;
using sm_application.Scripts.Main.Events;
using sm_application.Scripts.Main.Game;
using sm_application.Scripts.Main.Service;
using sm_application.Scripts.Main.Systems;
using UnityEngine;

namespace sm_application.Scripts.Main.Installers
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
            Services.Register<ControlService>(_controlServiceInstaller);
            Services.Register<ScreenService>(_screenServiceInstaller);
            Services.Register<PoolService>();
            Services.Register<DebugService>(_debugServiceInstaller);
            Services.Register<SceneLoaderService>();
            Services.Register<StatisticService>();
            Services.Register<AudioService>(_audioServiceInstaller);
            Services.Register<SettingsService>(_settingsServiceInstaller);
            Services.Register<GameStateService>();
            Services.Register<LocalizationService>();
            
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
            // SystemsService.Dispose();
            // Services.Dispose();
        }
        
        private IEnumerator LateStartup()
        {
            yield return null;
            new StartupSystemsLateInitEvent().Fire();
        }
    }
}