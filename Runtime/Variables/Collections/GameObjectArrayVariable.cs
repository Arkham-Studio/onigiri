using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName ="Variables/GameObject Array")]
    public class GameObjectArrayVariable : BaseArrayVariable<GameObject>
    {
        public override string ValueToString() => SelectedValue.name;
        public override int ValueToInt() => SelectedValue.GetInstanceID();
        public override float ValueToFloat() => SelectedValue.GetInstanceID();
        public override bool ValueToBool() => SelectedValue.activeSelf;
    }
}
