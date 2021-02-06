using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Denum Variables")]
public class DenumVariable : BaseVariable<Denum>
{
    [SerializeField] private Denum[] sequence;
    [SerializeField] private int sequenceIndex;


    public void NextDenum()
    {
        SetSequenceDenum(1);
    }

    public void PreviousDenum()
    {
        SetSequenceDenum(-1);
    }

    public void SetSequenceDenum(int _d)
    {
        sequenceIndex = (sequence.Length+(sequenceIndex+_d))% sequence.Length;
        SetValue(sequence[sequenceIndex]);
    }

}
