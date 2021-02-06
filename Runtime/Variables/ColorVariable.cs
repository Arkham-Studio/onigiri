using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Color")]
public class ColorVariable : BaseVariable<Color>
{

    public void ChangeColor(ColorVariable color)
    {
        Value = color.Value;
        OnChange();
    }

}
