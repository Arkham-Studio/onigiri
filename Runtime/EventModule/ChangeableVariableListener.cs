using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class ChangeableVariableListener : MonoBehaviour
{
    [SerializeField] private ChangeableVariablePack[] pack;

    //  MONOS
    private void OnEnable()
    {
        foreach (var item in pack)
            item.variable.onChange.AddListener(item.OnChange);
    }

    private void OnDisable()
    {
        foreach (var item in pack)
            item.variable.onChange.RemoveListener(item.OnChange);
    }

    //  UTILS
    [System.Serializable]
    public class ChangeableVariablePack
    {
        [Title("##############")]
        public ChangeableVariable variable;
        public UnityEvent response;

        [Button(ButtonSizes.Large)]
        public void OnChange() => response.Invoke();
    }
}
