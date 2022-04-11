using UnityEngine;

namespace Arkham.Onigiri.AI
{
    [CreateAssetMenu(menuName = "AI/State")]
    public class AIState : ScriptableObject
    {
        public AIAction[] actions;
        public AITransition[] transitions;

        public void UpdateState(AIStateController controller)
        {
            DoActions(controller);
            CheckTransitions(controller);
        }

        private void DoActions(AIStateController controller)
        {
            for (int i = 0; i < actions.Length; i++)
                actions[i].Act(controller);
        }

        private void CheckTransitions(AIStateController controller)
        {
            for (int i = 0; i < transitions.Length; i++)
                controller.TransitionToState(transitions[i].decision.Decide(controller) ? transitions[i].trueState : transitions[i].falseState);
        }
    }
}