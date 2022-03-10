using System;

namespace Arkham.Onigiri.Variables
{
    [Serializable]
    public class IntReference : BaseVariableReference<int, IntVariable>, IReferenceDrawer
    {
        public IntReference(int value) : base(value)
        {
        }
    }
}
