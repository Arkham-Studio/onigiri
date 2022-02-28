using Sirenix.OdinInspector;
using UnityEngine;

namespace Arkham.Onigiri.Physics2DModule
{
    [CreateAssetMenu(menuName = "Managers/PhysicsLayer Collision Manager 2D"), DefaultExecutionOrder(-1000)]
    public class PhysicsLayerCollisionManager2D : ScriptableObject
    {
        public LayerCollisionWrapper[] collisionsLayers = new LayerCollisionWrapper[0];

        [System.Serializable]
        public class LayerCollisionWrapper
        {
            [ValueDropdown("@UnityEditorInternal.InternalEditorUtility.layers")]
            public string layer;
            private string[] allLayers;
            public LayerMask others;
        }

        public void Awake()
        {
            foreach (LayerCollisionWrapper item in collisionsLayers)
                Physics2D.SetLayerCollisionMask(LayerMask.NameToLayer(item.layer), item.others);
        }
    }
}
