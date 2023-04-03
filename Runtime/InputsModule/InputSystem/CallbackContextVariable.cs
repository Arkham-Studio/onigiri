using UnityEngine;
using UnityEngine.InputSystem;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/CallbackContext")]
    public class CallbackContextVariable : BaseVariable<InputAction.CallbackContext>
    {
        [SerializeField] private bool listenStarted = false;
        [SerializeField] private bool listenPerformed = true;
        [SerializeField] private bool listenCancel = false;

        public override void OnChange()
        {
            if (Value.started && listenStarted)
                base.OnChange();
            else if (Value.performed && listenPerformed)
                base.OnChange();
            else if (Value.canceled && listenCancel)
                base.OnChange();
        }
    }

}