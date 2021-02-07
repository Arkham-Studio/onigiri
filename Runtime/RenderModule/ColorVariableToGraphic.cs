using Arkham.Onigiri.Variables;
using UnityEngine;
using UnityEngine.UI;
#pragma warning disable CS0649
namespace Arkham.Onigiri.RenderModule
{
    public class ColorVariableToGraphic : MonoBehaviour
    {
        [SerializeField] private Graphic MyGraphic;
        [SerializeField] private ColorVariable variable;
        [SerializeField] private bool initOnStart = true;

        private void Start()
        {
            if (initOnStart) UpdateRenderer();
        }

        private void OnValidate() => MyGraphic = MyGraphic ?? GetComponent<Graphic>();

        private void OnEnable() => variable.onChange.AddListener(UpdateRenderer);

        private void OnDisable() => variable.onChange.RemoveListener(UpdateRenderer);

        public void UpdateRenderer() => MyGraphic.color = variable.Value;
    }
}
