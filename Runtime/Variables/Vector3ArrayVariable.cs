using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Array/Vector3")]
    public class Vector3ArrayVariable : BaseVariable<Vector3[]>
    {

        public Vector3 this[int i]
        {
            get { return Value[i]; }
        }

        public int Lenght
        {
            get { return Value.Length; }
        }
    }
}
