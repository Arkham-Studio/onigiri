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

        public IntVariable playerCountValue;

        [SerializeField, ListDrawerSettings(CustomAddFunction = "OnNewInputPack", CustomRemoveElementFunction = "OnRemoveInputPack")]
        private List<InputActionPack> inputActionPacks = new List<InputActionPack>();

        private void OnEnable()
        {
            foreach (InputActionPack item in inputActionPacks)
                item.BindInputAsset();

            asset?.Enable();

        }

        private void OnDisable()
        {
            foreach (InputActionPack item in inputActionPacks)
                item.UnBindInputAsset();
        }

#if UNITY_EDITOR
        private void OnAssetChange()
        {
            foreach (var item in inputActionPacks)
            {
                item.Asset = asset;
            }
        }

        private void OnRemoveInputPack(InputActionPack _ia)
        {
            _ia.UnBindInputAsset();
            inputActionPacks.Remove(_ia);
        }

#endif


        [System.Serializable]
        class InputActionPack
        {
            private InputActionAsset asset;
            [ValueDropdown("GetAllActionMaps"), HideIf("@asset == null"), SerializeField, HorizontalGroup("map"), HideLabel, OnValueChanged("OnMapChange"), Space]
            string map;
            [ValueDropdown("GetAllActions"), HideIf("@asset == null"), HideIf("@string.IsNullOrEmpty(map)"), SerializeField, HorizontalGroup("map"), HideLabel, Space]
            string action;

            [SerializeField, GUIColor("@changeableVariable ? Color.white : Color.red")] private ChangeableVariable changeableVariable;
            [SerializeField] private IntVariable deviceID;

            private bool started = true;
            private bool performed = true;
            private bool canceled = true;

            [HorizontalGroup("buttons"), Button(ButtonSizes.Small, Name = "Started"), GUIColor("@started ? Color.white : Color.grey")]
            void ToggleStarted() => started = !started;
            [HorizontalGroup("buttons"), Button(ButtonSizes.Small, Name = "Performed"), GUIColor("@performed ? Color.white : Color.grey")]
            void TogglePerformed() => performed = !performed;
            [HorizontalGroup("buttons"), Button(ButtonSizes.Small, Name = "Canceled"), GUIColor("@canceled ? Color.white : Color.grey")]
            void ToggleCanceled() => canceled = !canceled;

            public InputActionPack(InputActionAsset asset)
            {
                this.asset = asset;
#if UNITY_EDITOR
                OnAssetChange();
#endif
            }

            public InputActionAsset Asset
            {
                set
                {
                    asset = value;
#if UNITY_EDITOR
                    OnAssetChange();
#endif
                }
            }

            public void Enable()
            {

                BindInputAsset();
            }

            public void Disable()
            {

                UnBindInputAsset();
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
            private void OnAssetChange()
            {
                action = "";
                map = asset?.actionMaps[0].name ?? "";
                OnMapChange();
            }
            private void OnMapChange()
            {
                action = asset?.FindActionMap(map)?.actions[0].name ?? "";
            }


            private IEnumerable<string> GetAllActions()
            {
                var _r = new List<string>();

                if (string.IsNullOrEmpty(map))
                    return _r;

                if (asset != null)
                {
                    foreach (var item in asset?.FindActionMap(map)?.actions)
                    {
                        _r.Add(item.name);
                    }
                }

                return _r;
            }
            private IEnumerable<string> GetAllActionMaps()
            {
                var _r = new List<string>();

                if (asset == null)
                    return _r;

                foreach (var item in asset?.actionMaps)
                {
                    _r.Add(item.name);
                }

                return _r;
            }
#endif
        }


#if UNITY_EDITOR


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

        private InputActionPack OnNewInputPack()
        {
            return new InputActionPack(asset);
        }
#endif
    }
}
