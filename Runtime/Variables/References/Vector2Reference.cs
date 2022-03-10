using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [Serializable]
    [InlineProperty(LabelWidth = 12)]
    public class Vector2Reference : BaseVariableReference<Vector2, Vector2Variable>, IReferenceDrawer
    {
        public Vector2Reference(Vector2 value) : base(value)
        {
        }
    }
}
