using sm_application.Extension;
using sm_application.Service;
using UnityEngine;

namespace sm_application.Context
{
    public class AppContext : MonoBehaviour
    {
        public static Transform Hierarchy;
        public static Transform ServicesHierarchy;
        
        private static string _initScene;

        private static bool _isInitialized;
        public static bool IsInitialized => _isInitialized;
        public static string InitScene => _initScene;

        public static void Instantiate()
        {
            Hierarchy = new GameObject() {name = "AppContext"}.transform;
            Hierarchy.gameObject.AddComponent<AppContext>();
            ServicesHierarchy = new GameObject() {name = "Services"}.transform;
            ServicesHierarchy.SetParent(Hierarchy);
            _isInitialized = true;
        }
        
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            #if UNITY_EDITOR
            UnityEditorUtility.ExpandScene(Hierarchy.gameObject.scene);
            UnityEditorUtility.ExpandHierarchyItem(Hierarchy);
            UnityEditorUtility.ExpandHierarchyItem(ServicesHierarchy);
            UnityEditorUtility.ExpandHierarchyItem(ServicesHierarchy);
            #endif
        }

        private void OnApplicationQuit()
        {
            SystemsService.DisposeAllSystems();
            Services.Dispose();
            _initScene = null;
            _isInitialized = false;
        }

        public static void SetInitScene(string sceneName)
        {
            _initScene ??= sceneName;
        }
    }
}