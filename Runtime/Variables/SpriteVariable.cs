using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName ="Variables/Sprite")]
    public class SpriteVariable : BaseVariable<Sprite>
    {
        public override string ValueToString(string format = "") => Value.name;
    }
}
