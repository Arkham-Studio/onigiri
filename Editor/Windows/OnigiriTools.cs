using Arkham.Onigiri.Events;
using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using System;
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
        [MenuItem("Onigiri/Tools")]
        private static void OpenWindow()
        {
            GetWindow<OnigiriTools>().Show();
        }

        OdinMenuTree _tree;

        protected override OdinMenuTree BuildMenuTree()
        {
            _tree = new OdinMenuTree();
            _tree.Selection.SupportsMultiSelect = false;

            //_tree.Selection.SelectionChanged += (e) => { _targets = null; };

            _tree.Add("Scripts Pack", new OnigiriScriptsPack());
            _tree.Add("PlugeableAI Pack", new OnigiriPlugeableAIPack());
            _tree.Add("Buttons Debug", new OnigiriPlayButtonDebug());
            _tree.AddAllAssetsAtPath("Game Events", "Assets/Scriptables/", typeof(GameEvent), true, true).SortMenuItemsByName();
            _tree.AddAllAssetsAtPath("Changeables Variables", "Assets/Scriptables/", typeof(ChangeableVariable), true, true).SortMenuItemsByName();

            //if(Selection.activeObject.GetType() == typeof(GameEvent))
            //_tree.Add("Manage Event", new ManageGameEvent<GameEvent>(Selection.activeObject as GameEvent));

            return _tree;
        }

        //protected override void DrawMenu()
        //{
        //    base.DrawMenu();
        //}



        //List<object> _targets;
        //protected override IEnumerable<object> GetTargets()
        //{
        //    if (_targets != null) return _targets;

        //    if (_tree.Selection != null)
        //    {
        //        if (_targets == null) _targets = new List<object>();
        //        if (_tree.Selection.SelectedValue?.GetType() == typeof(GameEvent))
        //        {
        //            _targets.Add(new ManageGameEvent<GameEvent>((GameEvent)_tree.Selection.SelectedValue));

        //            return _targets;
        //        }
        //        else if (_tree.Selection.SelectedValue.GetType().IsSubclassOf(typeof(ChangeableVariable)))
        //        {
        //            _targets.Add(new ManageGameEvent<ChangeableVariable>((ChangeableVariable)_tree.Selection.SelectedValue));
        //            return _targets;
        //        }
        //    }

        //    return base.GetTargets();
        //}


        //  MENU TREE
        public class OnigiriScriptsPack
        {
            [OnInspectorGUI("MyInspectorGUI", false)]
            public string packName;
            public bool controller = true;
            public bool manager = true;
            public bool data = false;

            private void MyInspectorGUI()
            {
                SirenixEditorGUI.Title("Onigiri Scripts Pack", "------------", TextAlignment.Center, true);
            }

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

            T o;

            List<GameObject> allObjects;
            List<ScriptableObject> allScriptables;

            List<ScriptableObject> scriptables;

            List<Component> component;
            List<Delegate> actions;



            public ManageGameEvent(T _o)
            {
                o = _o;
                EditorApplication.projectChanged += OnProjectChange;

                component = new List<Component>();
                scriptables = new List<ScriptableObject>();
                if (actions == null)
                    actions = new List<Delegate>();

                OnProjectChange();
                OnSelectionChange();

            }

            private void OnSelectionChange()
            {
                if (allObjects != null)
                {
                    foreach (var item in allObjects)
                    {
                        foreach (var _item in item.GetComponentsInChildren<Component>())
                        {
                            var _so = new SerializedObject(_item);
                            var _sp = _so.GetIterator();
                            while (_sp.NextVisible(true))
                            {
                                if (_sp.propertyType == SerializedPropertyType.ObjectReference)
                                {
                                    if (_sp.objectReferenceValue == o)
                                    {
                                        component.Add(_item);
                                    }
                                }
                            }
                        }
                    }
                }

                //if (allScriptables != null)
                //{
                //    foreach (var item in allScriptables)
                //    {
                //        var _so = new SerializedObject(item);
                //        var _sp = _so.GetIterator();

                //        while (_sp.Next(true))
                //        {

                //            if (_sp.propertyType == SerializedPropertyType.ObjectReference)
                //            {

                //                if (_sp.objectReferenceValue == o)
                //                {
                //                    scriptables.Add(item);
                //                }
                //            }
                //        }

                //    }
                //}

                //  actions if changeable variable
                if (Application.isPlaying)
                {
                    actions = GetActionFromVariable((object)o);
                }
            }

            public void OnProjectChange()
            {
                allObjects = new List<GameObject>();
                SceneManager.GetActiveScene().GetRootGameObjects(allObjects);

                allScriptables = new List<ScriptableObject>();
                foreach (var item in AssetDatabase.FindAssets("t:ScriptableObject", new string[1] { "Assets/Scriptables" }))
                {
                    var _p = AssetDatabase.GUIDToAssetPath(item);
                    var _o = AssetDatabase.LoadAssetAtPath(_p, typeof(ScriptableObject)) as ScriptableObject;
                    allScriptables.Add(_o);
                    //foreach (var _item in _o.GetType().GetFields())
                    //{
                    //    if (_item.FieldType != typeof(T)) continue;
                    //    if (_item.GetValue(item).Equals(o))
                    //        scriptables.Add(_o);
                    //    break;
                    //}

                }
            }

            [OnInspectorGUI]
            private void MyInspectorGUI()
            {
                SirenixEditorGUI.Title("Onigiri References Finder", "------------", TextAlignment.Center, true);


                if (component.Count > 0)
                {
                    GUILayout.Space(24);
                    SirenixEditorGUI.Title("Refs in Components", "", TextAlignment.Left, true);
                }
                foreach (var item in component)
                {
                    SirenixEditorFields.UnityObjectField(item.transform.root.name, item, typeof(MonoBehaviour), true);
                }

                if (actions.Count > 0)
                {
                    GUILayout.Space(24);
                    SirenixEditorGUI.Title("Event Listeners (onRaise or onChange)", "", TextAlignment.Left, true);
                }
                foreach (var item in actions)
                {
                    var _style = new GUILayout();

                    SirenixEditorGUI.BeginInlineBox(GUILayout.ExpandWidth(true));
                    //SirenixEditorFields.UnityObjectField(item.Target.ToString(), (UnityEngine.Object)item.Target, item.Target.GetType(), true);
                    SirenixEditorFields.PolymorphicObjectField(item.Target.ToString(), item.Target, item.Target.GetType(), true);
                    SirenixEditorFields.TextField("", item.Method.Name);
                    SirenixEditorGUI.EndInlineBox();
                }

                //if (o == null) return;

                if (allScriptables.Count > 0)
                {
                    GUILayout.Space(24);
                    SirenixEditorGUI.Title("Refs in Scriptables", "", TextAlignment.Left, true);
                }
                foreach (var item in allScriptables)
                {
                    //foreach (var _item in item.GetType().GetFields())
                    //{
                    //    if (_item.FieldType != typeof(T)) continue;
                    //    if (_item.GetValue(item).Equals(o))
                    SirenixEditorFields.UnityObjectField(item, typeof(ScriptableObject), true);
                    //}

                }
            }

        }

        public class OnigiriPlayButtonDebug
        {
            List<GameObject> allObjects;
            List<Button> allButtons;

            public OnigiriPlayButtonDebug()
            {

            }

            private void PopulateObjects()
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
                    GUILayout.Label("Need to be in play mode.");
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


        //  UTILS
        public static List<Delegate> GetActionFromVariable(object _obj)
        {
            List<Delegate> _actions = new List<Delegate>();

            if (_obj.GetType().IsSubclassOf(typeof(ChangeableVariable)))
            {
                ChangeableVariable _var = (ChangeableVariable)_obj;

                var _callsField = typeof(UnityEventBase).GetField("m_Calls", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                var _onChangeObject = _callsField.GetValue(_var.onChange);

                foreach (var _runtimeCallsField in _onChangeObject.GetType().GetRuntimeFields())
                {
                    if (_runtimeCallsField.Name != "m_RuntimeCalls")
                        continue;
                    IEnumerable<object> _runtimeCallsObjects = (IEnumerable<object>)_runtimeCallsField.GetValue(_onChangeObject);

                    foreach (var _callObject in _runtimeCallsObjects)
                    {
                        foreach (var _delegateField in _callObject.GetType().GetRuntimeFields())
                        {
                            _actions.Add((UnityAction)_delegateField.GetValue(_callObject));
                        }
                    }
                    break;
                }
            }
            else if (_obj.GetType().Equals(typeof(GameEvent)))
            {
                GameEvent _var = (GameEvent)_obj;

                foreach (var item in _var.GetType().GetRuntimeFields())
                {
                    if (!item.Name.Equals("actions"))
                        continue;
                    _actions.Add((Action)item.GetValue(_var));


                    //foreach (var _item in _actionsVarValue.GetType().GetRuntimeProperties())
                    //{
                    //    Debug.Log(_item.GetType());
                    //}
                    break;
                }

            }
            return _actions;
        }
    }
}