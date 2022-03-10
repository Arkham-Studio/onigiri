using System;

namespace Arkham.Onigiri.Variables
{
    [Serializable]
    public class FloatReference : BaseVariableReference<float, FloatVariable>, IReferenceDrawer
    {
        public FloatReference(float value) : base(value)
        {
        }
    }
}