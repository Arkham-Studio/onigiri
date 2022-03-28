#pragma warning disable CS0649
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Arkham.Onigiri.Inputs
{

    public class GenericInputsController : MonoBehaviour
    {

        [SerializeField] GenericInputsPack[] packs;

        void Update()
        {
            foreach (GenericInputsPack item in packs)
            {
                switch (item.mode)
                {
                    case GenericInputMode.keep:
                        if (Input.GetKey(item.key)) item.response.Invoke();
                        break;
                    case GenericInputMode.up:
                        if (Input.GetKeyUp(item.key)) item.response.Invoke();
                        break;
                    case GenericInputMode.down:
                        if (Input.GetKeyDown(item.key)) item.response.Invoke();
                        break;
                    default:
                        break;
                }
            }
        }

        [System.Serializable]
        public class GenericInputsPack
        {
            [HorizontalGroup(""), LabelWidth(50)]
            public KeyCode key;
            [HorizontalGroup(""), LabelWidth(50)]
            [ShowIf("@key != KeyCode.None")]
            public GenericInputMode mode;
            [ShowIf("@key != KeyCode.None")]
            public UnityEvent response;

        }

        public enum GenericInputMode { keep, up, down }
    }

}