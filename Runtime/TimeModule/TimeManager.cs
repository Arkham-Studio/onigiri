using Arkham.Onigiri.Variables;
using UnityEngine;

namespace Arkham.Onigiri.TimeModule
{
    [CreateAssetMenu(menuName = "Managers/Time Manager")]
    public class TimeManager : ScriptableObject
    {
        public FloatVariable enabledTime;
        public FloatVariable time;
        public IntVariable frameCount;

        public StringVariable date;
        public LongVariable longDate;
    }
}
