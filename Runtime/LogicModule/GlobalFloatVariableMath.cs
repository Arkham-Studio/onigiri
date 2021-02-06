using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Logic/Float Math")]
public class GlobalFloatVariableMath : ScriptableObject
{
#if UNITY_EDITOR
    [TextArea(2, 4), HideLabel()]
    public string infos;
#endif
    [SerializeField] private FloatReference a;
    [SerializeField] private FloatMathPack[] packs;
    [SerializeField] private FloatVariable output;
    public UnityEvent response;

    public void DoMath()
    {
        float _lastValue = a.Value;
        foreach (FloatMathPack item in packs)
            _lastValue = item.Calcultate(_lastValue);

        output.SetValue(_lastValue);

        response?.Invoke();
    }

    [System.Serializable]
    public class FloatMathPack
    {
        [SerializeField] private FloatReference b;
        [SerializeField] private FormulaType formula;

        public float Calcultate(float _a)
        {
            switch (formula)
            {
                case FormulaType.add:
                    return (_a + b.Value);

                case FormulaType.multiply:
                    return (_a * b.Value);

                case FormulaType.sub:
                    return (_a - b.Value);

                case FormulaType.divide:
                    return (_a / b.Value);

                case FormulaType.min:
                    return (Mathf.Min(_a, b.Value));

                case FormulaType.max:
                    return (Mathf.Max(_a, b.Value));

                case FormulaType.mean:
                    return ((_a + b.Value) * 0.5f);

                case FormulaType.abs:
                    return (Mathf.Abs(_a));

            }
            return _a;
        }
    }

    public enum FormulaType { add, multiply, sub, divide, min, max, mean, abs }
}
