using Sirenix.OdinInspector;
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
            [Title("@Event?.name")]
            public GameEvent Event;
            [ShowIf("Event")]
            public UnityEvent Response;

            [Button("Invoke Event",ButtonSizes.Large),ShowIf("Event")]
            public void OnEventRaised() => Response.Invoke();
        }
    }
}
