using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEventListener : MonoBehaviour
{

    public SpawnEventPack[] eventPacks;

    private void OnEnable()
    {
        foreach (SpawnEventPack item in eventPacks)
        {
            if (item.Event == null || item.prefab == null) continue;
            item.Event.RegisterDelegate(item.Spawn);
        }
    }

    private void OnDisable()
    {
        foreach (SpawnEventPack item in eventPacks)
        {
            if (item.Event == null || item.prefab == null) continue;
            item.Event.UnRegisterDelegate(item.Spawn);
        }
    }


    [System.Serializable]
    public class SpawnEventPack
    {
        public GameEvent Event;
        public Transform prefab;
        [ShowIf("parented")]
        public Transform parentTransform;
        public TransformVariable instantiatedTransform;

        public Vector3Reference position;
        public Vector3Reference rotation;
        public bool parented = false;
        public bool worldSpace = true;


        public void Spawn()
        {
            Vector3 p = (!worldSpace && parentTransform != null) ? p = parentTransform.position + position : position;
            Transform t = (parented && parentTransform != null) ? Instantiate(prefab, p, Quaternion.Euler(rotation)) : Instantiate(prefab, p, Quaternion.Euler(rotation), parentTransform.root);
            instantiatedTransform?.SetValue(t);
        }

    }
}
