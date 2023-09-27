using System;
using Newtonsoft.Json;
using sm_application.Extension;
using UnityEngine;

namespace sm_application.Wrappers
{
    public static class Serializer
    {
        public static string ToJson(object target)
        {
            return JsonConvert.SerializeObject(target);
        }

        public static T ParseScriptableObject<T>(string json) where T : ScriptableObject
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, new Common.ScriptableObjectConverter<T>());
            }
            catch (Exception e)
            {
                Log.Exception(e);
                return null;
            }
        }
        
        public static T Parse<T>(string json) 
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                Log.Exception(e);
                return default(T);
            }
        }
    }
}