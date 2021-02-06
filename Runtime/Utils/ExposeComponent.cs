using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExposeComponent : MonoBehaviour
{

    public Component component;
    public ComponentVariable componentVariable;

    private void Start()
    {

        if (component == null) Destroy(this);
        componentVariable.Value = component;
    }
}
