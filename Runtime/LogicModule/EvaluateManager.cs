using System;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

[CreateAssetMenu(menuName = "Managers/Evaluate Manager")]
public class EvaluateManager : ScriptableObject
{
    [SerializeField] private EvalPack[] evals;

    public void EvaluatePack(int _i)
    {
        if (evals.Length >= _i || _i < 0) return;
        evals[_i].Evaluate();
    }

    public void EvaluatePack(DenumVariable _d)
    {
        foreach (EvalPack item in evals)
            if (item.name == _d) item.Evaluate();
    }

    [Serializable]
    public class EvalPack
    {
#if UNITY_EDITOR
        [TextArea(2, 4), HideLabel()]
        public string infos;
#endif
        public DenumVariable name;
        public FloatReference input;
        public CurveVariable curve;
        public FloatVariable output;
        public UnityEvent response;

        public void Evaluate()
        {
            output.SetValue(curve.Value.Evaluate(input.Value));
            response.Invoke();
        }
    }
}