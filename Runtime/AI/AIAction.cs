using UnityEngine;

namespace Arkham.Onigiri.AI
{
    public abstract class AIAction : ScriptableObject
    {
        public abstract void Act(AIStateController controller);
    } 
}