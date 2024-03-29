using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace sm_application.Extension
{
    public static partial class Common
    {
        public static void ClearLogs()
        {
            #if UNITY_EDITOR
            var assembly = Assembly.GetAssembly(typeof(Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
            #endif
        }
        
        public static T FindComponentInScene<T>() where T : MonoBehaviour
        {
            var roots = SceneManager.GetActiveScene().GetRootGameObjects();
            
            foreach (var gameObject in roots)
            {
                if (gameObject.TryGetComponent<T>(out var result))
                {
                    return result;
                }

                var result2 = gameObject.GetComponentInChildren<T>();

                if (result2 != null)
                {
                    return result2;
                }
            }

            return null;
        }

        public static string CleanName(this GameObject gameObject)
        {
            gameObject.name = gameObject.name.Replace("(Clone)", "").Trim();
            return gameObject.name;
        }
        
        public static void SetScale(this RectTransform rectTransform, float x = float.NaN, float y = float.NaN, float z = float.NaN)
        {
            var localScale = rectTransform.localScale;
            if (float.IsNaN(x) == false) localScale = new Vector3(x, localScale.y, localScale.z);
            if (float.IsNaN(y) == false) localScale = new Vector3(localScale.x, y, localScale.z);
            if (float.IsNaN(z) == false) localScale = new Vector3(localScale.x, localScale.y, z);
            rectTransform.localScale = localScale;
        }
        
        public static void SetActive(this Scene scene, bool state)
        {
            foreach (var rootGameObject in scene.GetRootGameObjects())
            {
                rootGameObject.SetActive(state);
            }
        }
        
        public static void SwitchActive(this GameObject gameObject)
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        public static bool IsDestroyed(this GameObject gameObject)
        {
            return gameObject == null && !ReferenceEquals(gameObject, null);
        }

        public static Transform[] GetChildrenTransforms(this Transform transform)
        {
            var transforms = new Transform[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                transforms[i] = transform.GetChild(i);
            }

            return transforms;
        }
        
        public static TransformInfo GetTransformInfo(this Transform transform)
        {
            return new TransformInfo(transform);
        }
        
        public static TransformInfo[] GetChildrenTransformInfo(this Transform transform)
        {
            var infos = new TransformInfo[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                infos[i] = transform.GetChild(i).GetTransformInfo();
            }

            return infos;
        }
    }

    [Serializable]
    public struct TransformInfo
    {
        public Vector3 Position;
        public Vector3 LocalPosition;
        public Quaternion Rotation;
        public Quaternion LocalRotation;
        public Vector3 LocalScale;

        public TransformInfo(Transform transform)
        {
            Position = transform.position;
            LocalPosition = transform.localPosition;
            Rotation = transform.rotation;
            LocalRotation = transform.localRotation;
            LocalScale = transform.localScale;
        }
    }
}