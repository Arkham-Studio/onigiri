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


        public override string ValueToString() => Value.ToString();
        public override bool ValueToBool() => Value;
        public override float ValueToFloat() => Value ? 1 : 0;
        public override int ValueToInt() => Value ? 1 : 0;
    }
}

