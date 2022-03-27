#pragma warning disable CS0649
using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.LogicModule
{
    public class IntVariableMaths : MonoBehaviour
    {
        [SerializeField] private MathPack[] packs;

        public void CalculateMath(int _i)
        {
            if (_i >= packs.Length)
                return;

            packs[_i].DoMaths();
        }

        [System.Serializable]
        public class MathPack
        {
            [SerializeField] private Operation math;
            [SerializeField] private IntReference a;
            [SerializeField] private IntReference b;
            [SerializeField] private IntVariable c;
            public UnityEvent onMathDone;

            public void DoMaths()
            {
                switch (math)
                {
                    case Operation.add:
                        c.SetValue(a.Value + b.Value);
                        break;
                    case Operation.multiply:
                        c.SetValue(a.Value * b.Value);
                        break;
                    case Operation.divide:
                        c.SetValue(a.Value / b.Value);
                        break;
                    case Operation.min:
                        c.SetValue(Mathf.Min(a.Value, b.Value));
                        break;
                    case Operation.max:
                        c.SetValue(Mathf.Max(a.Value, b.Value));
                        break;
                    case Operation.mean:
                        c.SetValue((a.Value + b.Value) / 2);
                        break;
                    case Operation.abs:
                        c.SetValue(Mathf.Abs(a.Value));
                        break;
                    default:
                        break;
                }
                onMathDone.Invoke();
            }
        }

        public enum Operation { add, multiply, divide, min, max, mean, abs }
    }
}
