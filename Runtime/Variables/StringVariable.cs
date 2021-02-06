using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/String")]
    public class StringVariable : BaseVariable<string>
    {

        [SerializeField] private string decimalFormat = "0.00";

        [SerializeField, TextArea(4, 8)] private string textZone;


        private void OnValidate()
        {
            DefaultValue = textZone;
            SetValueQuiet(textZone);
        }


        /// <summary>
        /// Convert a Float to TextVariable value, rounded by the decimalFormat
        /// </summary>
        public void FloatToText(float _f) => SetValue(_f.ToString(decimalFormat));

        /// <summary>
        /// Convert a Int to TextVariable value
        /// </summary>
        public void IntToText(int _i) => SetValue(_i.ToString());

        /// <summary>
        /// Convert a FloatVariable to TextVariable value, rounded by the decimalFormat
        /// </summary>
        public void FloatVariableToText(FloatVariable _f) => SetValue(_f.Value.ToString(decimalFormat));

        /// <summary>
        /// Convert a IntVariable to TextVariable value
        /// </summary>
        public void IntVariableToText(IntVariable _i) => SetValue(_i.Value.ToString());

        public void FromStringVariable(StringVariable _s) => SetValue(_s.Value);
    }
}
