using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Vector2")]
public class Vector2Variable : BaseVariable<Vector2>
{
    public static implicit operator Vector2(Vector2Variable reference)
    {
        return reference.Value;
    }
}
