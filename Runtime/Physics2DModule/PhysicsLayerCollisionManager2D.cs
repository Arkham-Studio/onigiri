using Sirenix.OdinInspector;
using UnityEngine;

namespace Arkham.Onigiri.Physics2DModule
{
    [CreateAssetMenu(menuName = "Managers/PhysicsLayer Collision Manager 2D"), DefaultExecutionOrder(-1000)]
    public class PhysicsLayerCollisionManager2D : ScriptableObject
    {
        public LayerCollisionWrapper[] collisionsLayers = new LayerCollisionWrapper[0];

        //private static string[] physic2DLayerNames;

        [System.Serializable]
        public class LayerCollisionWrapper
        {
            [ValueDropdown("allLayers")]
            public string layer;

            private string[] allLayers;

            public LayerMask others;

#if UNITY_EDITOR
            public LayerCollisionWrapper()
            {
                allLayers = UnityEditorInternal.InternalEditorUtility.layers;
            } 
#endif
        }

        public void Awake()
        {
            //physic2DLayerNames = UnityEditorInternal.InternalEditorUtility.layers;

            foreach (LayerCollisionWrapper item in collisionsLayers)
            {
                Physics2D.SetLayerCollisionMask(LayerMask.NameToLayer(item.layer), item.others);
            }
        }
    }
}
