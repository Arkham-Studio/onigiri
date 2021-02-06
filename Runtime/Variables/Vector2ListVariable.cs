using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Array/Vector2")]
public class Vector2ArrayVariable : BaseVariable<Vector2[]>
{

    public Vector2 this[int i]
    {
        get { return Value[i]; }
        set { Value[i] = value; }
    }

    public int Lenght
    {
        get { return Value.Length; }
    }
}
