using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.AnimatorModule
{
    public class RandomEventStateBehavior : StateMachineBehaviour
    {

        public UnityEvent response;


        public void CheckRandom(float _chance)
        {
            if (_chance > Random.Range(0f, 1f))
            {
                Debug.Log(response.GetPersistentTarget(0).GetInstanceID());
                response.Invoke();
           
            }
        }
    }
}
