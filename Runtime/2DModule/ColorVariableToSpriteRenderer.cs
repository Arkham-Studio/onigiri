using UnityEngine;
using System.Collections;

public class ColorVariableToSpriteRenderer : MonoBehaviour
{

    [SerializeField] private SpriteRenderer mySpriteRenderer;
    [SerializeField] private ColorVariable value;
    [SerializeField] private bool initOnStart = true;

    private void OnEnable() => value.onChange.AddListener(UpdateValue);

    private void OnDisable() => value.onChange.RemoveListener(UpdateValue);

    void Start()
    {
        if (mySpriteRenderer == null) mySpriteRenderer = GetComponent<SpriteRenderer>();
        if (initOnStart) UpdateValue();
    }

    public void UpdateValue() => mySpriteRenderer.color = value.Value;

}
