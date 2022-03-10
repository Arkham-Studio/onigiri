using UnityEngine;
using Arkham.Onigiri.Utils;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Color")]
    public class ColorVariable : BaseVariable<Color>
    {

        public void ChangeColor(ColorVariable color) => SetValue(color.Value);

        public override string ValueToString() => Value.ToString();
        public override float ValueToFloat() => Value.Luminance();
        public override int ValueToInt() => Mathf.RoundToInt(Value.Luminance() * 100f);
        public override bool ValueToBool() => Value.Luminance() > 0;

    }
}
