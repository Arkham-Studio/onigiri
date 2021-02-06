using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.AnimatorModule
{
    public class AnimEventsListener : MonoBehaviour
    {

        public AnimEventPack[] responses;

        public void TriggerEventByIndex(int i)
        {
            if (i < 0 || i >= responses.Length) return;
            responses[i].trigger.Invoke();
        }

        public void TriggerEventByName(string s)
        {
            if (s == "") return;
            foreach (AnimEventPack item in responses)
            {
                if (item.name != s) continue;
                item.trigger.Invoke();
            }

        }


        [System.Serializable]
        public class AnimEventPack
        {
            public string name;
            public UnityEvent trigger;
        }

    }
}