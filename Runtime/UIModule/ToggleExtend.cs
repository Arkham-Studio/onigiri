using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleExtend : MonoBehaviour
{
    [SerializeField] private Toggle myToggle;

    public UnityEvent onTrue;
    public UnityEvent onFalse;

    private void Start()
    {
        if (myToggle == null) myToggle = GetComponent<Toggle>();
    }

    private void OnEnable() => myToggle.onValueChanged.AddListener(OnChange);
    private void OnDisable() => myToggle.onValueChanged.RemoveListener(OnChange);

    private void OnChange(bool arg0)
    {
        if (arg0) onTrue.Invoke();
        else onFalse.Invoke();
    }


}
