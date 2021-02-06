using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class UnityEventMarker : Marker, INotification, INotificationOptionProvider
{
    public UnityEvent response;
    [SerializeField] private bool emitOnce = false;
    [SerializeField] private bool retroactive = false;

    public PropertyName id { get; }
    public NotificationFlags flags => (emitOnce ? NotificationFlags.TriggerOnce : default) | (retroactive ? NotificationFlags.Retroactive : default);

}
