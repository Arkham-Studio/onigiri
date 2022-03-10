using Arkham.Onigiri.Variables;
using Sirenix.OdinInspector;
using System.Collections;
using UnityEngine;

namespace Arkham.Onigiri.Utils
{
    public class BaseComponentExtend<T> : MonoBehaviour
    {
        [Title("REFERENCES")]
        [SerializeField] protected T myExtendedComponent;
        [SerializeField] protected ChangeableVariable refVariable;

        protected void OnValidate() => PopulateRef();
        protected void Start() => PopulateRef();

        private void PopulateRef()
        {
            myExtendedComponent ??= GetComponent<T>();
            ((BaseVariable<T>)refVariable)?.SetValue(myExtendedComponent);
        }
    }
}