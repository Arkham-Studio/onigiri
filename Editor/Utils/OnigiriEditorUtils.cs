using Sirenix.OdinInspector.Editor;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Arkham.Onigiri.Editor
{

    public class OnigiriEditorUtils
    {
        public static UnityEngine.Object CreateSripteable(Type _type)
        {

            UnityEngine.Object _obj = (UnityEngine.Object)Activator.CreateInstance(_type);
            AssetDatabase.CreateAsset(_obj, "Assets/new.asset");

            return _obj;
        }
    } 

}
