using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Texture")]
    public class TextureVariable : BaseVariable<Texture>
    {
        public override string ValueToString() => Value.name;

    }
}
