using UnityEngine;
using UnityEngine.Playables;

namespace Arkham.Onigiri.TimelineModule
{
    public class GameEventReceiver : MonoBehaviour, INotificationReceiver
    {
        public void OnNotify(Playable origin, INotification notification, object context)
        {
            if (notification is GameEventMarker _marker) _marker.Raise();
        }
    }
}
