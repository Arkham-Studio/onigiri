using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Arkham.Onigiri.UI
{
    public class ButtonExtend : MonoBehaviour, IDeselectHandler
    {
        public UnityEvent onDeselect;

        public void OnDeselect(BaseEventData eventData)
        {
            onDeselect.Invoke();
        }

  
    }
}
