using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Texture")]
    public class TextureVariable : BaseVariable<Texture>
    {
        public override string ValueToString() => Value.name;

        public static implicit operator string(TextureVariable reference) => reference.Value.name;
        public static implicit operator Vector2(TextureVariable reference) => new Vector2(reference.Value.width, reference.Value.width);
    }
}
