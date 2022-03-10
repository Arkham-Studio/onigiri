using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Component")]
    public class ComponentVariable : BaseVariable<Component>
    {
        public override string ValueToString() => Value.name;
    }
}
