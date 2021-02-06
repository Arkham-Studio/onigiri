using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonExtend : MonoBehaviour, IDeselectHandler
{
    public UnityEvent onDeselect;

    public void OnDeselect(BaseEventData eventData)
    {
        onDeselect.Invoke();
    }

  
}
