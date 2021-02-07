#pragma warning disable CS0649
using Arkham.Onigiri.Variables;
using UnityEngine;

namespace Arkham.Onigiri.RenderModule
{
    [CreateAssetMenu(menuName = "Managers/ShaderGlobalManager", fileName = "ShaderGlobalManager")]
    public class ShaderGlobalManager : ScriptableObject
    {
        [SerializeField] private ShaderGlobalsPack[] shaderGlobals;


        //  MONOS
        public void OnEnable()
        {
            foreach (var item in shaderGlobals)
            {
                item.variable.onChange.AddListener(item.SetGlobal);
                item.propertieID = Shader.PropertyToID(item.variable.name);
                item.SetGlobal();
            }
        }

        private void OnDisable()
        {
            foreach (var item in shaderGlobals)
                item.variable.onChange.RemoveListener(item.SetGlobal);
        }


        //  UTILS
        [System.Serializable]
        public class ShaderGlobalsPack
        {

            public ChangeableVariable variable;
            [HideInInspector]
            public int propertieID;

            public void SetGlobal()
            {
                switch (variable)
                {
                    case FloatVariable f:
                        Shader.SetGlobalFloat(propertieID, f.Value);
                        break;
                    case IntVariable i:
                        Shader.SetGlobalInt(propertieID, i.Value);
                        break;
                    case Vector3Variable vvv:
                        Shader.SetGlobalVector(propertieID, vvv.Value);
                        break;
                    case TextureVariable t:
                        Shader.SetGlobalTexture(propertieID, t.Value);
                        break;
                }
            }
        }

    }
}
