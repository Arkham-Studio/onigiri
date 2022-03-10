using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/GameObject")]
    public class GameObjectVariable : BaseVariable<GameObject>
    {
        public override string ValueToString() => Value.name;
        public override int ValueToInt() => Value.GetInstanceID();
        public override float ValueToFloat() => Value.GetInstanceID();
        public override bool ValueToBool() => Value.activeSelf;
    }
}   