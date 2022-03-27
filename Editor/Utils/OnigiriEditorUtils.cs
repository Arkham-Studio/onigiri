using Arkham.Onigiri.Attributes;
using Sirenix.OdinInspector.Editor;
using System;
using UnityEditor;

namespace Arkham.Onigiri.Editor
{

    public class OnigiriEditorUtils
    {
        public static void CreateSripteable(InspectorProperty _p)
        {
            var _guid = AssetDatabase.FindAssets($"t:{_p.Info.TypeOfValue.Name} {_p.Info.PropertyName}", new string[] { "Assets/Scriptables" });
            if (_guid.Length == 0)
            {
                if (!AssetDatabase.IsValidFolder("Assets/Scriptables"))
                    AssetDatabase.CreateFolder("Assets", "Scriptables");

                var _path = $"Assets/Scriptables/{_p.Info.PropertyName}.asset";
                var _attr = _p.Parent.GetAttribute<VariablesManagerAttribute>();
                if (_attr != null)
                {
                    if (!AssetDatabase.IsValidFolder($"Assets/Scriptables/{_attr.folderName}"))
                        AssetDatabase.CreateFolder("Assets/Scriptables", _attr.folderName);
                    _path = $"Assets/Scriptables/{_attr.folderName}/{_p.Info.PropertyName}.asset";
                }

                UnityEngine.Object _obj = (UnityEngine.Object)Activator.CreateInstance(_p.Info.TypeOfValue);
                AssetDatabase.CreateAsset(_obj, _path);

                _p.BaseValueEntry.WeakSmartValue = _obj;
                _p.BaseValueEntry.ApplyChanges();
            }
            else
            {
                var _asset = AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(_guid[0]), _p.Info.TypeOfValue);
                if (_asset == null)
                    return;
                _p.BaseValueEntry.WeakSmartValue = _asset;
                _p.BaseValueEntry.ApplyChanges();
            }
        }
    }

}
