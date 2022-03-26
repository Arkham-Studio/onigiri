using Arkham.Onigiri.Attributes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Arkham.Onigiri.Editor
{
    public class OnigiriAssetPostProcessor : AssetPostprocessor
    {
        static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            //  CLEAR PRELOADED ASSETS
            var preloadedAssets = PlayerSettings.GetPreloadedAssets().ToList();
            foreach (string item in deletedAssets)
            {
                if (!item.Contains("Assets/Scriptables/"))
                    continue;
                
                for (int i = preloadedAssets.Count - 1; i >= 0; i--)
                {
                    if (preloadedAssets[i] != null)
                        continue;
                    preloadedAssets.RemoveAt(i);
                }
                PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());

                break;
            }


            //  ADD PRELOADED ASSETS
            foreach (string assetPath in importedAssets)
            {
                var obj = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
                var attr = obj?.GetType().GetCustomAttributes(true).ToList().Find(x => x.GetType() == typeof(PreloadAssetAttribute));
                
                if (attr == null)
                    continue;

                if (preloadedAssets.Contains(obj))
                    return;

                preloadedAssets.Add(obj);
            }
            PlayerSettings.SetPreloadedAssets(preloadedAssets.ToArray());


            //  EDITOR ICONS
            var metaChangedForAssets = new List<string>();
            foreach (string assetPath in importedAssets)
            {
                var attr = AssetDatabase.LoadAssetAtPath<MonoScript>(assetPath)?.GetClass()?.GetCustomAttributes(true).ToList().Find(x => x.GetType() == typeof(EditorIconAttribute));
                
                if (attr == null)
                    continue;

                // Find guid based on icon name from attr
                var iconName = ((EditorIconAttribute)attr).Name;
                var iconGuids = AssetDatabase.FindAssets($"{iconName} t:texture2D");
                var iconGuidsList = iconGuids.ToList();
                var guid = iconGuidsList.FirstOrDefault();

                if (string.IsNullOrEmpty(guid))
                    continue;

                // Read meta for script
                var metaPath = $"{assetPath}.meta";
                var scriptMetaTextLines = File.ReadAllLines(metaPath);
                var metaIconLine = $"icon: {{fileID: 2800000, guid: {guid}, type: 3}}";
                for (var i = 0; i < scriptMetaTextLines.Length; ++i)
                {
                    var line = scriptMetaTextLines[i];
                    // Find icon line
                    if (line.Contains("icon: ") && !line.Contains(metaIconLine))
                    {
                        var indexIconKeyName = line.IndexOf("icon: ");
                        var indexAfterClosingBrace = line.IndexOf("}", indexIconKeyName) + 1;
                        var newLine = line.Replace(line.Substring(indexIconKeyName, indexAfterClosingBrace - indexIconKeyName), metaIconLine);
                        scriptMetaTextLines[i] = newLine;
                        File.WriteAllLines(metaPath, scriptMetaTextLines);
                        metaChangedForAssets.Add(assetPath);
                        break;
                    }
                }
            }

            // We need to reimport all assets where the meta was changed
            if (metaChangedForAssets.Count > 0)
            {
                foreach (var assetPath in metaChangedForAssets)
                {
                    AssetDatabase.ImportAsset(assetPath);
                }
            }
        }
    }
}
