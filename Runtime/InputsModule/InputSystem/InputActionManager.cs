using Arkham.Onigiri.Attributes;
using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Arkham.Onigiri.InputSystem
{
    [CreateAssetMenu(menuName = "Managers/Input Manager"), PreloadAsset, EditorIcon("onigiri-icon-m")]
    public class InputActionManager : ScriptableObject
    {
        [ValueDropdown("GetAllScriptableObjects", AppendNextDrawer = true), OnValueChanged("OnAssetChange")]
        public InputActionAsset asset;

        [FoldoutGroup("advanced")]
        public IntVariable playerCountValue;

        [SerializeField, ListDrawerSettings(CustomAddFunction = "OnNewInputPack", CustomRemoveElementFunction = "OnRemoveInputPack", NumberOfItemsPerPage = 5)]
        private List<InputActionPack> inputActionPacks = new List<InputActionPack>();

        private void OnEnable()
        {
            if (asset != null)
            {
                foreach (InputActionPack item in inputActionPacks)
                {
                    item.Asset = asset;
                    item.BindInputAsset();
                }
            }
            asset?.Enable();
        }

        private void OnDisable()
        {
            if (asset != null)
            {
                foreach (InputActionPack item in inputActionPacks)
                {
                    item.UnBindInputAsset();
                    item.Asset = null;
                }
            }
            asset?.Disable();
        }

#if UNITY_EDITOR
        private void OnAssetChange()
        {
            foreach (var item in inputActionPacks)
            {
                item.Asset = asset;
            }
        }

        private InputActionPack OnNewInputPack()
        {
            if (asset != null)
                return new InputActionPack(asset);
            else return null;
        }

        private void OnRemoveInputPack(InputActionPack _ia)
        {
            _ia.UnBindInputAsset();
            inputActionPacks.Remove(_ia);
        }

        private static IEnumerable GetAllScriptableObjects()
        {
            return UnityEditor.AssetDatabase.FindAssets("t:InputActionAsset")
                .Select(x => UnityEditor.AssetDatabase.GUIDToAssetPath(x))
                .Select(x =>
                {
                    var _o = UnityEditor.AssetDatabase.LoadAssetAtPath<ScriptableObject>(x);
                    return new ValueDropdownItem(_o.name, _o);
                });
        }

#endif

        [System.Serializable]
        class InputActionPack
        {
            [SerializeField, HideInInspector]
            private InputActionAsset asset;
            [ValueDropdown("GetAllActionMaps"), SerializeField, HorizontalGroup("map", Title = "$GetTitle"), HideLabel, Space, GUIColor("GetActionMapGUI")]
            string map;
            [ValueDropdown("GetAllActions"), SerializeField, HorizontalGroup("map"), HideLabel, Space, GUIColor("GetActionGUI")]
            string action;

            [SerializeField, GUIColor("@changeableVariable ? Color.white : Color.red")] private ChangeableVariable changeableVariable;
            [SerializeField, FoldoutGroup("advanced")] private IntVariable deviceID;

            [SerializeField, HideInInspector] private bool started = true;
            [SerializeField, HideInInspector] private bool performed = true;
            [SerializeField, HideInInspector] private bool canceled = true;

            [HorizontalGroup("buttons"), Button(ButtonSizes.Small, Name = "Started"), GUIColor("@started ? Color.white : Color.grey")]
            void ToggleStarted() => started = !started;
            [HorizontalGroup("buttons"), Button(ButtonSizes.Small, Name = "Performed"), GUIColor("@performed ? Color.white : Color.grey")]
            void TogglePerformed() => performed = !performed;
            [HorizontalGroup("buttons"), Button(ButtonSizes.Small, Name = "Canceled"), GUIColor("@canceled ? Color.white : Color.grey")]
            void ToggleCanceled() => canceled = !canceled;

            public InputActionPack(InputActionAsset asset)
            {
                this.asset = asset;
            }

            public InputActionAsset Asset
            {
                set => asset = value;
            }

            public void BindInputAsset()
            {
                if (asset == null || string.IsNullOrEmpty(map) || string.IsNullOrEmpty(map))
                    return;
                if (asset?.FindAction($"{map}/{action}") == null)
                    return;

                if (started)
                    asset[$"{map}/{action}"].started += OnPerformed;
                if (performed)
                    asset[$"{map}/{action}"].performed += OnPerformed;
                if (canceled)
                    asset[$"{map}/{action}"].canceled += OnCanceled;
            }

            public void UnBindInputAsset()
            {
                if (asset == null || string.IsNullOrEmpty(map) || string.IsNullOrEmpty(map))
                    return;
                if (asset?.FindAction($"{map}/{action}") == null)
                    return;

                if (started)
                    asset[$"{map}/{action}"].performed -= OnPerformed;
                if (performed)
                    asset[$"{map}/{action}"].performed -= OnPerformed;
                if (canceled)
                    asset[$"{map}/{action}"].canceled -= OnCanceled;

            }

            private void OnPerformed(InputAction.CallbackContext ctx)
            {
                if (!IsCorrectDevice(ctx))
                    return;

                switch (changeableVariable)
                {
                    case CallbackContextVariable _ctx:
                        _ctx.SetValue(ctx);
                        break;
                    default:
                        switch (ctx.ReadValueAsObject())
                        {
                            case float _f:
                                ((IVariableValueFrom)changeableVariable)?.FloatToValue(_f);
                                break;
                            case int _i:
                                ((IVariableValueFrom)changeableVariable)?.IntToValue(_i);
                                break;
                            case Vector2 _v:
                                ((IVariableValueFrom)changeableVariable)?.Vector2ToValue(_v);
                                break;
                            case bool _b:
                                ((IVariableValueFrom)changeableVariable)?.BoolToValue(_b);
                                break;
                        }
                        break;
                }
            }

            private void OnCanceled(InputAction.CallbackContext ctx)
            {
                if (!IsCorrectDevice(ctx))
                    return;

                switch (changeableVariable)
                {
                    case CallbackContextVariable _ctx:
                        _ctx.SetValue(ctx);
                        break;
                    case BoolVariable _b:
                        _b.SetValue(false);
                        break;
                    default:
                        ((IVariableResetable)changeableVariable)?.ResetValue();
                        break;
                }
            }

            bool IsCorrectDevice(InputAction.CallbackContext ctx)
            {
                if (deviceID == null)
                    return true;

                if (deviceID.Value <= 0)
                    return false;

                if (ctx.control.device.deviceId != deviceID.Value)
                    return false;

                return true;
            }


#if UNITY_EDITOR

            private string GetTitle { get => string.IsNullOrEmpty(map) || string.IsNullOrEmpty(action) || changeableVariable == null ? "ERROR" : $"{map.ToUpper()}/{action.ToUpper()}"; }

            private IEnumerable<string> GetAllActions()
            {
                var _r = new List<string>();

                if (asset != null && !string.IsNullOrEmpty(map) && asset.FindActionMap(map) != null)
                    foreach (var item in asset.FindActionMap(map).actions)
                        _r.Add(item.name);

                return _r;
            }

            private Color GetActionGUI() => !string.IsNullOrEmpty(map) && !string.IsNullOrEmpty(action) && asset?.FindActionMap(map)?.FindAction(action) != null ? Color.white : Color.red;
            private Color GetActionMapGUI() => !string.IsNullOrEmpty(map) && asset?.FindActionMap(map) != null ? Color.white : Color.red;

            private IEnumerable<string> GetAllActionMaps()
            {
                var _r = new List<string>();

                if (asset != null)
                    foreach (var item in asset.actionMaps)
                        _r.Add(item.name);

                return _r;
            }
#endif
        }

    }
}

