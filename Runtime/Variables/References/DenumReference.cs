using Arkham.Onigiri.Utils;

namespace Arkham.Onigiri.Variables
{
    [System.Serializable]
    public class DenumReference : BaseVariableReference<Denum, DenumVariable>, IReferenceDrawer
    {
        public DenumReference(Denum value) : base(value)
        {
        }
    }
}