using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/CallbackContext")]
    public class CallbackContextVariable : BaseVariable<InputAction.CallbackContext>
    {
        [SerializeField] private bool listenToPerformed = true;
        [SerializeField] private bool listenToCancel = false;

        public override void OnChange()
        {
            if (Value.performed && listenToPerformed)
                base.OnChange();
            else if (Value.canceled && listenToCancel)
                base.OnChange();

        }
    }

}