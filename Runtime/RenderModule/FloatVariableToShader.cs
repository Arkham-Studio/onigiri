using System.Collections;
using System.Collections.Generic;
using Arkham.Onigiri.Variables;
using UnityEngine;

[ExecuteInEditMode]
public class FloatVariableToShader : MonoBehaviour
{
    [SerializeField] private Renderer myRenderer;
    [SerializeField] private string propertieName;
    [SerializeField] private FloatVariable variable;
    [SerializeField] private bool isGlobal = true;

    private void Start()
    {
        if (myRenderer == null) myRenderer = GetComponent<Renderer>();
        UpdateVector3Variable();
    }

    private void OnEnable()
    {
        variable.onChange.AddListener(UpdateVector3Variable);
    }

    private void OnDisable()
    {
        variable.onChange.RemoveListener(UpdateVector3Variable);
    }

    public void UpdateVector3Variable()
    {
        if (!isGlobal)
            myRenderer.material.SetFloat(propertieName, variable.Value);
        else
            myRenderer.sharedMaterial.SetFloat(propertieName, variable.Value);
    }
}
