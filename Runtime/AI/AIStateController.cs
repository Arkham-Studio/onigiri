using UnityEngine;

namespace Arkham.Onigiri.AI
{
    public class AIStateController : MonoBehaviour
    {
        [SerializeField] protected AIState currentState;
        [SerializeField] protected bool isActive = true;

        protected float stateTimeElapsed;

        public virtual void Update()
        {
            if (isActive)
            {
                stateTimeElapsed += Time.deltaTime;
                currentState.UpdateState(this);
            }
        }

        public virtual void TransitionToState(AIState _nextState)
        {
            if (_nextState != null)
            {
                currentState = _nextState;
                OnExitState();
            }
        }

        public virtual bool CheckIfCountDownElapsed(float _duration) => (stateTimeElapsed >= _duration);
        public virtual void OnExitState() => stateTimeElapsed = 0;

    }
}