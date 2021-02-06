using Arkham.Onigiri.Variables;
using UnityEngine;

[ExecuteInEditMode]
public class ChangeableVariableToShader : MonoBehaviour
{

    [SerializeField] private Renderer myRenderer;
    [SerializeField] private string propertieName;
    [SerializeField] private ChangeableVariable variable;
    [SerializeField] private bool isGlobal = true;

    private Material mat;

    private void OnEnable()
    {
        if (myRenderer == null) myRenderer = GetComponent<Renderer>();
        mat = isGlobal ? myRenderer.sharedMaterial : myRenderer.material;

        variable.onChange.AddListener(UpdateVector3Variable);
        UpdateVector3Variable();
    }

    private void OnDisable()
    {
        variable.onChange.RemoveListener(UpdateVector3Variable);
    }

    public void UpdateVector3Variable()
    {

        switch (variable)
        {
            case Vector3Variable v3:
                mat.SetVector(propertieName, v3.Value);
                break;
            case FloatVariable f:
                mat.SetFloat(propertieName, f.Value);
                break;
            case TextureVariable t:
                mat.SetTexture(propertieName, t.Value);
                break;
        }
    }
}
