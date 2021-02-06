using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

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
#if UNITY_EDITOR
        [TextArea(2, 4), HideLabel()]
        public string infos;
#endif
        public KeyCode key;
        public GenericInputMode mode;
        public UnityEvent response;
    }

    public enum GenericInputMode { keep, up, down }
}
