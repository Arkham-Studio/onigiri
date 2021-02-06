using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [CreateAssetMenu(menuName = "Variables/Min Max")]
    public class MinMaxVariable : ScriptableObject
    {

        public float minValue;
        public float maxValue;

        public float Value(float t)
        {
            return Mathf.Lerp(minValue, maxValue, t);
        }

    }
}
