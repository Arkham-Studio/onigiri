using UnityEngine;
using UnityEngine.Playables;

namespace Arkham.Onigiri.TimelineModule
{
    public class UnityEventReceiver : MonoBehaviour, INotificationReceiver
    {
        public void OnNotify(Playable origin, INotification notification, object context)
        {
            if (notification is UnityEventMarker _marker) _marker.response.Invoke();
        }
    }
}
