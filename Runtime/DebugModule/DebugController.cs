using Arkham.Onigiri.Variables;
using Arkham.Onigiri.UI;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable CS0649

/*
 * ----------------- TOFINISH ---------------------
 * 
 * Instantie automatiquement une UI basée sur les type des Variables
 * 
 * 
 */

namespace Arkham.Onigiri.DebugModule
{
    public class DebugController : MonoBehaviour
    {

        [Title("REFERENCES")]
        [SerializeField] private DebugManager m_DebugManager;
        [SerializeField] private Transform content;

        [Title("PREFABS")]
        [SerializeField] private Transform sliderPrefab;
        [SerializeField] private Transform togglePrefab;

        [Title("VARIABLES")]
        [SerializeField] private ChangeableVariable[] allVariables;

        void Start()
        {
            foreach (var item in allVariables)
            {
                switch (item)
                {
                    case IntVariable i:
                        var _o = Instantiate(sliderPrefab, content);
                        foreach (var _item in _o.GetComponentsInChildren<ChangeableVariableToText>())
                            _item.Variable = i;
                        _o.GetComponentInChildren<ChangeableVariableToSlider>().Variable = i;
                        _o.GetComponentInChildren<Slider>().wholeNumbers = true;
                        _o.GetComponentInChildren<Slider>().onValueChanged.AddListener((b) => i.SetValue((int)b));
                        break;
                    case FloatVariable f:
                        _o = Instantiate(sliderPrefab, content);
                        foreach (var _item in _o.GetComponentsInChildren<ChangeableVariableToText>())
                            _item.Variable = f;
                        _o.GetComponentInChildren<ChangeableVariableToSlider>().Variable = f;
                        _o.GetComponentInChildren<Slider>().wholeNumbers = false;
                        _o.GetComponentInChildren<Slider>().onValueChanged.AddListener((b) => f.SetValue(b));
                        break;
                    case StringVariable s:
                        break;
                    case BoolVariable b:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
