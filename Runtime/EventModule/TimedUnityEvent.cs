using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using Arkham.Onigiri.Variables;

namespace Arkham.Onigiri.Events
{
    public class TimedUnityEvent : MonoBehaviour
    {

        [Title("SETTINGS")]
        [SerializeField] private FloatReference delay = new FloatReference(0f);
        [SerializeField] private FloatReference interval = new FloatReference(1f);
        private float nextTime;

        [Title("EVENT")]
        public UnityEvent eventOnTime;


        //  MONOS
        private void OnEnable() => nextTime = Time.time + delay;

        public void Update()
        {
            if (Time.time < nextTime) return;

            nextTime = Time.time + interval;
            eventOnTime.Invoke();
        }

    }
}
