using Arkham.Onigiri.Variables;
using UnityEngine;

namespace Arkham.Onigiri.Utils
{
    public class ExposeComponent : MonoBehaviour
    {
        public Component component;
        public ComponentVariable componentVariable;

        private void OnEnable()
        {
            if (component != null) componentVariable.SetValue(component);
        }
    }
}
