using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Vector2Int")]
    public class Vector2IntVariable : BaseVariable<Vector2Int>
    {
        public static implicit operator Vector2Int(Vector2IntVariable reference)
        {
            return reference.Value;
        }
    }
}
