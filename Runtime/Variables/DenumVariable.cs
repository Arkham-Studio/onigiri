#pragma warning disable CS0649
using Arkham.Onigiri.Utils;
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Denum Variables")]
    public class DenumVariable : BaseVariable<Denum>
    {
        [SerializeField] private Denum[] sequence;
        [SerializeField] private int sequenceIndex;


        public void NextDenum() => SetSequenceDenum(1);

        public void PreviousDenum() => SetSequenceDenum(-1);

        public void SetSequenceDenum(int _d)
        {
            sequenceIndex = (sequence.Length+(sequenceIndex+_d))% sequence.Length;
            SetValue(sequence[sequenceIndex]);
        }

        public override string ValueToString() => Value.name.ToString();
        public override int ValueToInt() => int.TryParse(Value.name, out int _r) ? _r : 0;

    }
}
