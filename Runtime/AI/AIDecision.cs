using UnityEngine;

namespace Arkham.Onigiri.AI
{
    public abstract class AIDecision : ScriptableObject
    {
        public abstract bool Decide(AIStateController controller);
    }
}