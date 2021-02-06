using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Vector3")]
    public class Vector3Variable : BaseVariable<Vector3>
    {
        public static implicit operator Vector3(Vector3Variable reference)
        {
            return reference.Value;
        }
    }
}

