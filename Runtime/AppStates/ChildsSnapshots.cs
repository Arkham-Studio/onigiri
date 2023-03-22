using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Arkham.Onigiri.Variables;

namespace Arkham.Onigiri.AppStates
{
    public class ChildsSnapshots : MonoBehaviour
    {
        [HideLabel, HideInInspector]
        public string lastId;

        [SerializeField, HideLabel, HorizontalGroup("changeable", Title = "@\"LAST ACTIVE ID - \"+lastId")] private ChangeableVariable changeableVariable;
        private bool onStart = true;

        [ListDrawerSettings(CustomAddFunction = "RecordNew")]
        public List<SnapshotPack> snapshots;

        private int actualSnapshotIndex = -1;
        private int lastSnapshotIndex = -1;


        private void Start()
        {
            if (onStart && changeableVariable != null)
                OnVariableChange();
        }

        private void OnEnable()
        {
            changeableVariable?.onChange.AddListener(OnVariableChange);
        }

        private void OnDisable()
        {
            changeableVariable?.onChange.RemoveListener(OnVariableChange);
        }

        void OnVariableChange()
        {
            switch (changeableVariable)
            {
                case StringVariable _s:
                    ActivateSnapshot(_s.Value);
                    break;
                case DenumVariable _d:
                    ActivateSnapshot(_d.Value.name);
                    break;
                case null:
                    return;
                default:
                    ActivateSnapshot(changeableVariable.name);
                    break;
            }
        }

        [Button("On Start"), GUIColor("@onStart ? Color.white : Color.grey"), HorizontalGroup("changeable")]
        void ToggleOnStart() => onStart = !onStart;

        public void ActivateSnapshot(string _id)
        {
            foreach (var item in snapshots)
            {
                if (item.id != _id)
                    continue;
                item.ActivateSnapshot();
            }
        }



#if UNITY_EDITOR
        private Dictionary<GameObject, bool> lastGameObjectStates;
        private Dictionary<Component, bool> lastComponentsStates;

        void RecordNew()
        {
            snapshots.Add(new SnapshotPack(gameObject, this));
            snapshots[snapshots.Count - 1].RecordSnapshot();
        }

        [Button("Enable All", ButtonSizes.Large), PropertyOrder(-1), HorizontalGroup("buttons")]
        void ActiveAll()
        {
            ToggleAll(true);
        }

        [Button("Disable All", ButtonSizes.Large), PropertyOrder(-1), HorizontalGroup("buttons")]
        void DisableAll()
        {
            ToggleAll(false);
        }

        void ToggleAll(bool _isEnable)
        {
            SetIndex(null);

            Transform[] _allTransform = GetComponentsInChildren<Transform>(true);
            foreach (Transform item in _allTransform)
            {
                if (item == transform)
                    continue;
                item.gameObject.SetActive(_isEnable);
            }

            Component[] _allComponents = GetComponentsInChildren<Component>(true);
            foreach (Component item in _allComponents)
            {
                switch (item)
                {
                    case MonoBehaviour _m:
                        _m.enabled = _isEnable;
                        break;
                    case Collider _m:
                        _m.enabled = _isEnable;
                        break;
                    case Collider2D _m:
                        _m.enabled = _isEnable;
                        break;
                    default:
                        continue;
                }
            }
        }

        [Button("Undo", ButtonSizes.Large), PropertyOrder(-1), HorizontalGroup("buttons"), EnableIf("@actualSnapshotIndex != lastSnapshotIndex")]
        void Revert()
        {
            if (lastComponentsStates == null || lastGameObjectStates == null)
                return;

            if (lastSnapshotIndex == -1)
                ActiveAll();
            else
                snapshots[lastSnapshotIndex]?.ActivateSnapshot();

        }

        public void SetIndex(SnapshotPack _snp)
        {
            lastSnapshotIndex = actualSnapshotIndex;
            actualSnapshotIndex = _snp == null ? -1 : snapshots.IndexOf(_snp);

            lastId = _snp == null ? "" : _snp.id;
        }

        public void SaveLast(SnapshotPack _snp)
        {
            SetIndex(_snp);
            if (lastSnapshotIndex != -1)
            {
                lastGameObjectStates = snapshots[lastSnapshotIndex].gameObjectStates;
                lastComponentsStates = snapshots[lastSnapshotIndex].componentsStates;
            }
        }
#endif

        [System.Serializable]
        public class SnapshotPack
        {
            private ChildsSnapshots controller;
            private GameObject parent;
            public Dictionary<GameObject, bool> gameObjectStates;
            public Dictionary<Component, bool> componentsStates;

            [HideLabel, HorizontalGroup("buttons")]
            public string id;


            public SnapshotPack(GameObject parent, ChildsSnapshots controller)
            {
                this.controller = controller;
                this.parent = parent;
                this.id = Random.Range(0, 100).GetHashCode().ToString();
            }


            [Button("ACTIVATE", ButtonSizes.Medium)]
            public void ActivateSnapshot()
            {
#if UNITY_EDITOR
                controller.SaveLast(this); 
#endif

                if (gameObjectStates == null || componentsStates == null)
                {
                    Debug.Log("States List not set");
                    return;
                }


                foreach (KeyValuePair<GameObject, bool> item in gameObjectStates)
                {
                    item.Key.SetActive(item.Value);
                }

                foreach (KeyValuePair<Component, bool> item in componentsStates)
                {
                    switch (item.Key)
                    {
                        case MonoBehaviour _m:
                            _m.enabled = item.Value;
                            break;
                        case Collider _m:
                            _m.enabled = item.Value;
                            break;
                        case Collider2D _m:
                            _m.enabled = item.Value;
                            break;
                        default:
                            continue;
                    }
                }
            }

            [Button("RECORD"), HorizontalGroup("buttons")]
            public void RecordSnapshot()
            {
#if UNITY_EDITOR
                controller.SetIndex(this); 
#endif

                gameObjectStates = new Dictionary<GameObject, bool>();
                componentsStates = new Dictionary<Component, bool>();

                Transform[] _allTransform = parent.GetComponentsInChildren<Transform>(true);
                foreach (Transform item in _allTransform)
                {
                    gameObjectStates.Add(item.gameObject, item.gameObject.activeSelf);
                }

                Component[] _allComponents = parent.GetComponentsInChildren<Component>(true);
                foreach (Component item in _allComponents)
                {
                    switch (item)
                    {
                        case MonoBehaviour _m:
                            componentsStates.Add(item, _m.enabled);
                            break;
                        case Collider _m:
                            componentsStates.Add(item, _m.enabled);
                            break;
                        case Collider2D _m:
                            componentsStates.Add(item, _m.enabled);
                            break;
                        default:
                            continue;
                    }
                }
            }
        }
    }

}
