using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Arkham.Onigiri.Editor
{
    public class OnigiriButtonsDebugger : OdinEditorWindow
    {
        [MenuItem("Onigiri/Button Debugger")]
        public static void OpenWindow()
        {
            GetWindow<OnigiriButtonsDebugger>("Button Debugger").Show();
        }

        private List<GameObject> allObjects;

        private new void OnGUI()
        {
            if (!EditorApplication.isPlaying)
                return;

            allObjects = new List<GameObject>();
            for (int i = 0; i < SceneManager.sceneCount; i++)
                allObjects.AddRange(SceneManager.GetSceneAt(i).GetRootGameObjects());



            GUILayout.Label("BUTTONS");
            GUILayout.Label("-------------");

            foreach (GameObject item in allObjects)
            {
                foreach (Button _item in item.GetComponentsInChildren<Button>(true))
                {
                    if (!_item.gameObject.activeSelf || !_item.IsActive())
                        continue;
                    GUILayout.Label(_item.transform.parent.parent.name + " > " + _item.transform.parent.name);
                    if (GUILayout.Button(_item.name))
                        _item.onClick.Invoke();
                }
            }

        }
    }
}
