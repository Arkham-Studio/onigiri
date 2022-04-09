using Arkham.Onigiri.Events;
using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Arkham.Onigiri.Editor
{
    public class OnigiriTools : OdinMenuEditorWindow
    {
        OdinMenuTree _tree;
        List<ScriptableObject> allScriptables;
        List<GameObject> allObjectsInScene;
        List<GameObject> allPrefabs;

        [MenuItem("Onigiri/Tools")]
        private static void OpenWindow() => GetWindow<OnigiriTools>().Show();

        private void OnProjectChange() => UpdateAllRefs();

        protected override OdinMenuTree BuildMenuTree()
        {
            UpdateAllRefs();

            _tree = new OdinMenuTree();
            _tree.Selection.SupportsMultiSelect = false;

            _tree.Selection.SelectionChanged += (e) =>
            {
                if (e == SelectionChangedType.ItemAdded)
                    UpdateAllScriptablesRefs();
            };

            _tree.Add("Scripts Pack", new OnigiriScriptsPack());
            _tree.Add("PlugeableAI Pack", new OnigiriPlugeableAIPack());
            _tree.Add("Buttons Debug", new OnigiriPlayButtonDebug());


            foreach (ScriptableObject item in allScriptables)
            {
                if (item == null)
                    continue;

                switch (item)
                {
                    case ChangeableVariable _c:
                        _tree.Add($"Scriptables/Variables/{item.name}", new ManageGameEvent<ChangeableVariable>((ChangeableVariable)item, allScriptables, allObjectsInScene, allPrefabs));
                        break;
                    case GameEvent _e:
                        _tree.Add($"Scriptables/Events/{item.name}", new ManageGameEvent<GameEvent>((GameEvent)item, allScriptables, allObjectsInScene, allPrefabs));
                        break;
                    default:
                        string _managerFolderName = (item.GetType().Name.Split("Manager").Length >= 2 ? "Managers" : "Other");
                        _tree.Add($"Scriptables/{_managerFolderName}/{item.name}", new ManageGameEvent<ScriptableObject>(item, allScriptables, allObjectsInScene, allPrefabs));
                        break;
                }
            }

            return _tree;
        }

        private void Update()
        {
            if (MenuTree?.Selection == null)
                return;

            if (MenuTree.Selection?.FirstOrDefault()?.Name == "Buttons Debug" && Application.isPlaying)
            {
                ((OnigiriPlayButtonDebug)MenuTree.Selection.SelectedValue).PopulateObjects();
            }
        }

        private void UpdateAllScriptablesRefs()
        {
            switch (MenuTree.Selection.SelectedValue)
            {
                case ManageGameEvent<ChangeableVariable> _v:
                    _v.OnSelectionChange();
                    break;
                case ManageGameEvent<GameEvent> _v:
                    _v.OnSelectionChange();
                    break;
                case ManageGameEvent<ScriptableObject> _v:
                    _v.OnSelectionChange();
                    break;
            }
        }
        private void UpdateAllRefs()
        {
            if (allObjectsInScene == null)
                allObjectsInScene = new List<GameObject>();
            allObjectsInScene.Clear();
            SceneManager.GetActiveScene().GetRootGameObjects(allObjectsInScene);

            if (allScriptables == null)
                allScriptables = new List<ScriptableObject>();
            allScriptables.Clear();
            foreach (string _guid in AssetDatabase.FindAssets("t:ScriptableObject", new string[1] { "Assets/Scriptables/" }))
                allScriptables.Add(AssetDatabase.LoadAssetAtPath<ScriptableObject>(AssetDatabase.GUIDToAssetPath(_guid)));
            allScriptables.OrderBy(x => x.name);

            if (allPrefabs == null)
                allPrefabs = new List<GameObject>();
            allPrefabs.Clear();
            foreach (string _guid in AssetDatabase.FindAssets("t:Prefab", new string[1] { "Assets/Prefabs/" }))
                allPrefabs.Add(AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(_guid)));
            allPrefabs.OrderBy(x => x.name);
        }

        //  MENU TREE
        public class OnigiriScriptsPack
        {
            [OnInspectorGUI("MyInspectorGUI", false)]
            public string packName;
            public bool controller = true;
            public bool manager = true;
            public bool data = false;

            private void MyInspectorGUI() => SirenixEditorGUI.Title("Onigiri Scripts Pack", "------------", TextAlignment.Center, true);

            [Button(ButtonSizes.Large)]
            public void Create()
            {
                if (!Directory.Exists("Assets/Scripts"))
                    Directory.CreateDirectory("Assets/Scripts");
                if (!Directory.Exists("Assets/Scripts/" + packName))
                    Directory.CreateDirectory("Assets/Scripts/" + packName);
                if (!Directory.Exists("Assets/Scriptables/"))
                    Directory.CreateDirectory("Assets/Scriptables/");


                if (controller)
                {
                    string _controller = File.ReadAllText("Packages/Onigiri/Editor/Templates/ScriptPack/OnigiriControllerTemplate.txt");
                    _controller = _controller.Replace("#CONTROLLERNAME#", packName + "Controller");
                    _controller = _controller.Replace("#MANAGERNAME#", manager ? packName + "Manager" : "");
                    File.WriteAllText("Assets/Scripts/" + packName + "/" + packName + "Controller.cs", _controller);
                }

                if (manager)
                {
                    string _manager = File.ReadAllText("Packages/Onigiri/Editor/Templates/ScriptPack/OnigiriManagerTemplate.txt");
                    _manager = _manager.Replace("#NAME#", packName);
                    _manager = _manager.Replace("#MANAGERNAME#", packName + "Manager");
                    File.WriteAllText("Assets/Scripts/" + packName + "/" + packName + "Manager.cs", _manager);
                }

                if (data)
                {
                    string _data = File.ReadAllText("Packages/Onigiri/Editor/Templates/ScriptPack/OnigiriDataTemplate.txt");
                    _data = _data.Replace("#DATANAME#", packName + "Data");
                    File.WriteAllText("Assets/Scripts/" + packName + "/" + packName + "Data.cs", _data);
                }

                AssetDatabase.Refresh();
            }
        }

        public class OnigiriPlugeableAIPack
        {
            [OnInspectorGUI("MyInspectorGUI", false)]
            public string packName;

            private void MyInspectorGUI()
            {
                SirenixEditorGUI.Title("Onigiri PlugeableAI Pack", "------------", TextAlignment.Center, true);
            }

            [Button(ButtonSizes.Large)]
            public void Create()
            {
                string _action = System.IO.File.ReadAllText(
                    "Packages/Onigiri/Editor/Templates/PlugeableAI/PlugeableAI_Action_template.txt");
                string _decision = System.IO.File.ReadAllText(
                    "Packages/Onigiri/Editor/Templates/PlugeableAI/PlugeableAI_Decision_template.txt");
                string _transition = System.IO.File.ReadAllText(
                    "Packages/Onigiri/Editor/Templates/PlugeableAI/PlugeableAI_Transition_template.txt");
                string _state = System.IO.File.ReadAllText(
                    "Packages/Onigiri/Editor/Templates/PlugeableAI/PlugeableAI_State_template.txt");
                string _controller = System.IO.File.ReadAllText(
                    "Packages/Onigiri/Editor/Templates/PlugeableAI/PlugeableAI_StateController_template.txt");

                string _controllerType = packName + "StateControllerAI";

                _action = _action.Replace("#NAME#", packName);
                _action = _action.Replace("#CONTROLLERTYPE#", _controllerType);

                _decision = _decision.Replace("#NAME#", packName);
                _decision = _decision.Replace("#CONTROLLERTYPE#", _controllerType);

                _transition = _transition.Replace("#NAME#", packName);
                _transition = _transition.Replace("#CONTROLLERTYPE#", _controllerType);

                _state = _state.Replace("#NAME#", packName);
                _state = _state.Replace("#CONTROLLERTYPE#", _controllerType);

                _controller = _controller.Replace("#NAME#", packName);
                _controller = _controller.Replace("#CONTROLLERTYPE#", _controllerType);

                if (!Directory.Exists("Assets/Scripts"))
                    Directory.CreateDirectory("Assets/Scripts");
                if (!Directory.Exists("Assets/Scripts/PlugeableAI"))
                    Directory.CreateDirectory("Assets/Scripts/PlugeableAI");

                System.IO.File.WriteAllText("Assets/Scripts/PlugeableAI" + packName + "Action.cs", _action);
                System.IO.File.WriteAllText("Assets/Scripts/PlugeableAI" + packName + "Decision.cs", _decision);
                System.IO.File.WriteAllText("Assets/Scripts/PlugeableAI" + packName + "State.cs", _state);
                System.IO.File.WriteAllText("Assets/Scripts/PlugeableAI" + packName + "Transition.cs", _transition);
                System.IO.File.WriteAllText("Assets/Scripts/PlugeableAI" + _controllerType + ".cs", _controller);

                AssetDatabase.Refresh();
            }
        }


        public class ManageGameEvent<T> where T : UnityEngine.Object
        {
            [SerializeField, HideLabel]
            T o;

            List<GameObject> allObjects;
            List<GameObject> allPrefabs;
            List<ScriptableObject> allScriptables;
            List<ScriptableObject> scriptables;
            List<Component> component;
            List<Delegate> actions;

            public ManageGameEvent(T _event, List<ScriptableObject> _allScriptables, List<GameObject> _allObjects, List<GameObject> _allPrefabs)
            {
                o = _event;

                allObjects = new List<GameObject>(_allObjects);
                allPrefabs = new List<GameObject>(_allPrefabs);
                allScriptables = new List<ScriptableObject>(_allScriptables);

                scriptables = new List<ScriptableObject>();
                component = new List<Component>();
                actions = new List<Delegate>();

                OnSelectionChange();
            }


            public void OnSelectionChange()
            {
                //  filter component
                component.Clear();
                foreach (var item in allObjects)
                {
                    foreach (var _item in item.GetComponentsInChildren<Component>(true))
                    {
                        var _so = new SerializedObject(_item);
                        var _sp = _so.GetIterator();
                        while (_sp.NextVisible(true))
                        {
                            if (_sp.propertyType == SerializedPropertyType.ObjectReference && _sp.objectReferenceValue == o)
                                component.Add(_item);
                        }
                    }
                }


                //  filter prefabs components
                foreach (var item in allPrefabs)
                {
                    foreach (var _item in item.GetComponentsInChildren<Component>(true))
                    {
                        var _so = new SerializedObject(_item);
                        var _sp = _so.GetIterator();
                        while (_sp.NextVisible(true))
                        {
                            if (_sp.propertyType == SerializedPropertyType.ObjectReference && _sp.objectReferenceValue == o)
                                component.Add(_item);
                        }
                    }
                }

                //  filter scripteable
                scriptables.Clear();
                foreach (var item in allScriptables)
                {
                    var _so = new SerializedObject(item);
                    var _sp = _so.GetIterator();

                    while (_sp.Next(true))
                    {
                        if (_sp.propertyType == SerializedPropertyType.ObjectReference && _sp.objectReferenceValue == o)
                            scriptables.Add(item);
                    }
                }

                //  filter event actions
                //if (!Application.isPlaying)
                //    return;
                //actions = GetActionFromVariable(o);
            }


            [OnInspectorGUI]
            private void MyInspectorGUI()
            {

                SirenixEditorGUI.Title($"{component.Count} Refs in Components", "", TextAlignment.Left, true);
                GUI.color = Color.white;

                foreach (var item in component)
                {
                    GUI.color = ((Behaviour)item).enabled ? Color.white : Color.grey;
                    SirenixEditorFields.UnityObjectField(item, typeof(Behaviour), true);
                }

                SirenixEditorGUI.Title($"{scriptables.Count} Refs in Scriptables", "", TextAlignment.Left, true);
                GUI.color = Color.white;

                foreach (var item in scriptables)
                {
                    SirenixEditorFields.UnityObjectField(item, typeof(ScriptableObject), true);
                }
            }

        }

        public class OnigiriPlayButtonDebug
        {
            List<GameObject> allObjects;
            List<Button> allButtons;

            public OnigiriPlayButtonDebug()
            {
                PopulateObjects();
            }


            public void PopulateObjects()
            {
                allButtons = new List<Button>();
                allObjects = new List<GameObject>();
                for (int i = 0; i < SceneManager.sceneCount; i++)
                    allObjects.AddRange(SceneManager.GetSceneAt(i).GetRootGameObjects());
                foreach (GameObject item in allObjects)
                    foreach (Button _item in item.GetComponentsInChildren<Button>(true))
                        allButtons.Add(_item);
            }

            [OnInspectorGUI]
            private void MyInspectorGUI()
            {
                SirenixEditorGUI.Title("Onigiri Play Mode Buttons List", "------------", TextAlignment.Center, true);
                if (!EditorApplication.isPlaying)
                {
                    allObjects = null;
                    allButtons = null;
                    SirenixEditorGUI.Title("Need to be in play mode.", "", TextAlignment.Left, true);
                    return;
                }

                if (allObjects == null && allButtons == null)
                {
                    PopulateObjects();
                }

                foreach (Button _item in allButtons)
                {
                    if (!_item.gameObject.activeSelf || !_item.IsActive())
                        continue;
                    GUILayout.Label(_item.transform.root.name + " > " + _item.transform.parent.name);
                    if (GUILayout.Button(_item.name))
                        _item.onClick.Invoke();
                    GUILayout.Space(8);
                }
            }
        }

    }
}