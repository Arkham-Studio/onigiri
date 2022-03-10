using Arkham.Onigiri.Variables;

namespace Arkham.Onigiri.LogicModule
{
    public class IfStringVariable : BaseIfChangeableVariable<string, StringVariable, StringReference>
    {

        public override bool Test(string _value, ComparePack _pack)
        {
            switch (_pack.how)
            {
                case CompareOperation.equals:
                    return _value.Equals(_pack.toThat.Value);
                case CompareOperation.different:
                    return !_value.Equals(_pack.toThat.Value);
                case CompareOperation.less:
                    return _value.Length < _pack.toThat.Value.Length;
                case CompareOperation.more:
                    return _value.Length > _pack.toThat.Value.Length;
                case CompareOperation.lessEqual:
                    return _value.Length <= _pack.toThat.Value.Length;
                case CompareOperation.moreEqual:
                    return _value.Length >= _pack.toThat.Value.Length;
                default:
                    return false;
            }
        }


    }
}
