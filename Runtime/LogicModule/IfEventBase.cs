using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public abstract class IfEventBase<T, U> : MonoBehaviour where U : BaseVariable<T>
{

    public T test;
    [SerializeField] private U toCompare;

    [SerializeField] private bool onStart = true;
    [SerializeField] private bool isInversed = false;

    public UnityEvent onTrue, onFalse;
    public BoolUnityEvent onDynamic;

    private void OnEnable()
    {
        toCompare.onChange.AddListener(Compare);
        if (!onStart) return;
        Compare();
    }

    private void OnDisable() => toCompare.onChange.RemoveListener(Compare);

    public void Compare()
    {
        if (toCompare == null) return;

        bool _result = Test(toCompare.Value) ^ isInversed;

        if (_result) onTrue.Invoke();
        else onFalse.Invoke();

        onDynamic.Invoke(isInversed ? !(_result) : _result);
    }

    public void CompareWith(T _v)
    {
        bool _result = Test(_v) ^ isInversed;

        if (_result) onTrue.Invoke();
        else onFalse.Invoke();

        onDynamic.Invoke(_result);
    }

    public abstract bool Test(T _v);

    public void SetToTestValue(T _i) => test = _i;

    public enum CompareOperation { equals, less, more, lessEqual, moreEqual, different }

}
