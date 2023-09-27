using System;
using UnityEngine;

namespace sm_application.Extension
{
    public class LabeledArrayAttribute : PropertyAttribute
    {
        public readonly string[] names;

        public LabeledArrayAttribute(string[] names)
        {
            this.names = names;
        }

        public LabeledArrayAttribute(Type enumType)
        {
            names = Enum.GetNames(enumType);
        }
    }
}