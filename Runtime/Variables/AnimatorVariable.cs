using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Animator Variables")]
public class AnimatorVariable : BaseVariable<Animator>
{

    public void SetTrigger(string _v) => currentValue.SetTrigger(_v);
    public void ResetTrigger(string _v) => currentValue.ResetTrigger(_v);
    public void SetBool(string _n, bool _v) => currentValue.SetBool(_n, _v);
    public void SetInt(string _n, int _v) => currentValue.SetInteger(_n, _v);
    public void SetFloat(string _n, float _v) => currentValue.SetFloat(_n, _v);

    public void Play(string _n, int _l) => currentValue.Play(_n, _l);
    public void Rebind() => currentValue.Rebind();

}
