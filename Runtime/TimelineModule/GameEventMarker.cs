#pragma warning disable CS0649
using Arkham.Onigiri.Events;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Arkham.Onigiri.TimelineModule
{
    [CustomStyle("GameEventMarker")]
    public class GameEventMarker : Marker, INotification, INotificationOptionProvider
    {

        [SerializeField] private GameEvent gameEvent;
        [SerializeField] private bool emitOnce = false;
        [SerializeField] private bool retroactive = false;

        public void Raise() => gameEvent?.Raise();

        public NotificationFlags flags => (emitOnce ? NotificationFlags.TriggerOnce : default) | (retroactive ? NotificationFlags.Retroactive : default);

        public PropertyName id { get; }
    }
}
