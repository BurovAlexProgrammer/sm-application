using Cysharp.Threading.Tasks;
using sm_application.Constants;
using sm_application.Wrappers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace sm_application.Context
{
    public class SceneContext : MonoBehaviour
    {
        [SerializeField] private SceneContextInstaller[] _sceneContextInstallers;
        
        private void Awake()
        {
            if (!AppContext.IsInitialized)
            {
                AppContext.SetInitScene(SceneManager.GetActiveScene().name);
                PreloadBootScene();
                return;
            }
            
            for (var i = 0; i < _sceneContextInstallers.Length; i++)
            {
                _sceneContextInstallers[i].Construct();
            }
        }

        private void OnDestroy()
        {
            for (var i = 0; i < _sceneContextInstallers.Length; i++)
            {
                _sceneContextInstallers[i].Dispose();
            }
        }
        
        private async void PreloadBootScene()
        {
            SceneManager.LoadScene(App.BootScene);

            while (!AppContext.IsInitialized)
            {
                await UniTask.NextFrame();
            }
            
            SceneManager.LoadScene(AppContext.InitScene);
        }
    }
}