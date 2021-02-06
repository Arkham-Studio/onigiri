using Sirenix.OdinInspector;
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Bool")]
    public class BoolVariable : BaseVariable<bool>
    {
        [Button]
        public void ToggleValue()
        {
            Value = !Value;
            OnChange();
        }
    }
}

