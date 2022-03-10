using System;
using UnityEngine;

namespace Arkham.Onigiri.Variables
{
    [Serializable]
    public class ColorReference : BaseVariableReference<Color, ColorVariable>, IReferenceDrawer
    {
        public ColorReference(Color value) : base(value)
        {
        }
    }
}