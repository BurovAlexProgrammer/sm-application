﻿using sm_application.Extension;
using sm_application.Service;
using UnityEngine;

namespace sm_application.Game
{
    public class AppContext : MonoBehaviour
    {
        public static Transform Hierarchy;
        public static Transform ServicesHierarchy;

        private static bool _isExist;
        public static bool IsExist => _isExist;

        public static void Instantiate()
        {
            Hierarchy = new GameObject() {name = "AppContext"}.transform;
            Hierarchy.gameObject.AddComponent<AppContext>();
            ServicesHierarchy = new GameObject() {name = "Services"}.transform;
            ServicesHierarchy.SetParent(Hierarchy);
            _isExist = true;
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
            _isExist = false;
        }
    }
}