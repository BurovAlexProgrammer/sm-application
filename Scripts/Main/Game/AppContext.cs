using sm_application.Scripts.Main.Service;
using smApplication.Scripts.Extension;
using UnityEngine;

namespace sm_application.Scripts.Main.Game
{
    public class AppContext : MonoBehaviour
    {
        public static Transform Hierarchy;
        public static Transform ServicesHierarchy;

        public static void Instantiate()
        {
            Hierarchy = new GameObject() {name = "AppContext"}.transform;
            Hierarchy.gameObject.AddComponent<AppContext>();
            ServicesHierarchy = new GameObject() {name = "Services"}.transform;
            ServicesHierarchy.SetParent(Hierarchy);
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
            SystemsService.Dispose();
            Services.Dispose();
        }
    }
}