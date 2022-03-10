using System;
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [Serializable]
    public class Vector3Reference : BaseVariableReference<Vector3, Vector3Variable>, IReferenceDrawer
    {
        public Vector3Reference(Vector3 value) : base(value)
        {
        }
    }
}