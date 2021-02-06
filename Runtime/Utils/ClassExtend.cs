using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

public static class ClassExtend
{
    public static Vector3Int RoundToInt(this Vector3 _vector) => new Vector3Int(Mathf.RoundToInt(_vector.x), Mathf.RoundToInt(_vector.y), Mathf.RoundToInt(_vector.z));

    public static T GetRandom<T>(this T[] array) => array[Random.Range(0, array.Length)];

    public static int GetRandomIndex(this Array array) => Random.Range(0, array.Length);

    public static T GetRandom<T>(this List<T> list) => list[Random.Range(0, list.Count)];

    public static bool ValueFrom1D(this bool[,] table, int index, int columns)
    {
        var _v = index.ConvertTo2D(columns);
        return table[_v.x, _v.y];
    }

    public static Vector2Int ConvertTo2D(this int index, int columns)
    {
        var _y = Mathf.FloorToInt(index / columns);
        var _x = index - (_y * columns);
        return new Vector2Int(_x, _y);
    }

    public static float GetRandomInRange(this Vector2 vector) => Random.Range(vector.x, vector.y);

#if UNITY_EDITOR
    public static T LoadScripteableObject<T>(string _filter) where T : ScriptableObject
    {
        return (T)AssetDatabase.LoadAssetAtPath(
            AssetDatabase.GUIDToAssetPath(
                AssetDatabase.FindAssets(_filter)[0]
                ),
            typeof(T));

    }

    public static T[] LoadAllScripteableObject<T>(string _filter) where T : ScriptableObject
    {
        List<T> _result = new List<T>();
        foreach (var item in AssetDatabase.FindAssets(_filter))
        {
            var _o = (T)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(item), typeof(T));
            if (_o == null) continue;
            _result.Add(_o);
        }

        return _result.ToArray();

    }

    // Used by ScripteableObject to autoPopulate their scripteableObject Properties
    public static void CreateEmptyScripteables(this ScriptableObject _o)
    {
            var so = new SerializedObject(_o);
            var sp = so.GetIterator();
            sp.Next(true);
            sp.NextVisible(false);  //ignore first is mono
            while (sp.NextVisible(false))
            {
                System.Reflection.FieldInfo fi = _o.GetType().GetField(sp.propertyPath);
                if (sp.objectReferenceValue == null && fi.FieldType.IsSubclassOf(typeof(ScriptableObject)))
                {
                    var __o = ScriptableObject.CreateInstance(fi.FieldType);
                    AssetDatabase.CreateAsset(__o, "Assets/" + sp.name + ".asset");
                    so.FindProperty(sp.propertyPath).objectReferenceValue = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(AssetDatabase.GetAssetPath(__o));
                }
            }
            so.ApplyModifiedProperties();
            AssetDatabase.SaveAssets();
    }

#endif
}
