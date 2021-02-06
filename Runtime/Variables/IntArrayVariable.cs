using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Array/Int")]
public class IntArrayVariable : BaseVariable<int[]>
{
    public int Length
    {
        get
        {
            return Value.Length;
        }
    }

    public int this[int i]
    {
        get { return Value[i]; }
    }

}
