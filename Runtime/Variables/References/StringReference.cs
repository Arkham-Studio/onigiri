
using System;

namespace Arkham.Onigiri.Variables
{
    [Serializable]
    public class StringReference : BaseVariableReference<string, StringVariable>, IReferenceDrawer
    {
        public StringReference(string value) : base(value)
        {
        }
    }
}