using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Int")]
public class IntVariable : BaseVariable<int>
{

    public void ApplyChange(int amount)
    {
        Value += amount;
        if (amount != 0)
            OnChange();
    }

    public void ApplyChange(IntVariable amount)
    {
        Value += amount.Value;
        if (amount.Value != 0)
            OnChange();
    }
}
