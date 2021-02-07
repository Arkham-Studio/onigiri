#pragma warning disable CS0649
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.Events
{
    public class GameEventListener : MonoBehaviour
    {

        [SerializeField] private GameEventPack[] eventPacks;

        private void OnEnable()
        {
            foreach (GameEventPack item in eventPacks)
                item.Event?.RegisterDelegate(item.OnEventRaised);
        }

        private void OnDisable()
        {
            foreach (GameEventPack item in eventPacks)
                item.Event?.UnRegisterDelegate(item.OnEventRaised);
        }

        [System.Serializable]
        public class GameEventPack
        {
            public GameEvent Event;
            public UnityEvent Response;

            public void OnEventRaised() => Response.Invoke();
        }
    }
}
