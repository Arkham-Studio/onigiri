using UnityEngine;

public class ColorVariableToRenderer : MonoBehaviour
{
    [SerializeField] private Renderer myRenderer;
    [SerializeField] private ColorVariable variable;
    [SerializeField] private bool initOnStart = true;

    private void Start()
    {
        if (initOnStart) UpdateRenderer();
    }

    private void OnValidate() => myRenderer = myRenderer ?? GetComponent<Renderer>();

    private void OnEnable() => variable.onChange.AddListener(UpdateRenderer);

    private void OnDisable() => variable.onChange.RemoveListener(UpdateRenderer);

    public void UpdateRenderer() => myRenderer.material.color = variable.Value;
}
