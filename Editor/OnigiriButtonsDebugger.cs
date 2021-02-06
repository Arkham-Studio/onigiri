using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnigiriButtonsDebugger : OdinEditorWindow
{
    [MenuItem("Onigiri/Button Debugger")]
    public static void OpenWindow()
    {
        GetWindow<OnigiriButtonsDebugger>("Button Debugger").Show();
    }

    private List<GameObject> allObjects;

    private void OnGUI()
    {
        if (!EditorApplication.isPlaying) return;

        allObjects = new List<GameObject>();
        SceneManager.GetActiveScene().GetRootGameObjects(allObjects);


        GUILayout.Label("BUTTONS");
        GUILayout.Label("-------------");

        foreach (GameObject item in allObjects)
        {
            foreach (Button _item in item.GetComponentsInChildren<Button>(true))
            {
                if (!_item.gameObject.activeSelf || !_item.IsActive()) continue;
                GUILayout.Label(_item.transform.parent.parent.name + " > " + _item.transform.parent.name);
                if (GUILayout.Button(_item.name)) _item.onClick.Invoke();
            }
        }

    }
}
