using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.Events
{
    [CreateAssetMenu(fileName = "NewGameEvent", menuName = "Events/Game Event")]
    [InlineEditor(InlineEditorObjectFieldModes.Foldout, DrawHeader = false)]
    [HideMonoScript]
    public class GameEvent : ScriptableObject
    {
        //private readonly List<IEventListener> eventListeners = new List<IEventListener>();
        private Action actions = null;

        [PropertyOrder(10)]
        public UnityEvent response;

        [EnableIf("@UnityEngine.Application.isPlaying"), Button(ButtonSizes.Large)]
        public void Raise()
        {
            //for (int i = eventListeners.Count - 1; i >= 0; i--) eventListeners[i].OnEventRaised();
            actions?.Invoke();
            response.Invoke();
        }

        //public void RegisterListener(IEventListener listener)
        //{
        //    if (!eventListeners.Contains(listener))
        //        eventListeners.Add(listener);
        //}

        //public void UnregisterListener(IEventListener listener)
        //{
        //    if (eventListeners.Contains(listener))
        //        eventListeners.Remove(listener);
        //}

        public void RegisterDelegate(Action action) => actions += action;

        public void UnRegisterDelegate(Action action) => actions -= action;
    }
}
