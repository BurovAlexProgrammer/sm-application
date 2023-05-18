using UnityEngine;

namespace smApplication.Scripts.Extension.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class ReadOnlyFieldAttribute : PropertyAttribute { }
}