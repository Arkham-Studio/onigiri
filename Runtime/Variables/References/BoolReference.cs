using System;

namespace Arkham.Onigiri.Variables
{
    [Serializable]
    public class BoolReference : BaseVariableReference<bool, BoolVariable>, IReferenceDrawer
    {
        public BoolReference(bool value) : base(value)
        {
        }
    }
}