using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkham.Onigiri.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class VariablesManagerAttribute : Attribute
    {
        public string folderName;
    }
}
