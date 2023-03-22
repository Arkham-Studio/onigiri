using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Component")]
    public class ComponentVariable : BaseVariable<Component>
    {
        public override string ValueToString(string format = "") => Value.name;
    }
}
