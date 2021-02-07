using System.Collections.Generic;
using UnityEngine;

namespace Arkham.Onigiri.PoolsModule
{
    [CreateAssetMenu(fileName = "NewPoolManager", menuName = "Managers/Pool Manager")]
    public class PoolManager : ScriptableObject
    {
        public ObjectToPool[] objectsToPool;

        private Transform mainPool;
        private Dictionary<string, List<GameObject>> pooledObjects;

        public void Init()
        {
            pooledObjects = new Dictionary<string, List<GameObject>>();
            if (objectsToPool.Length == 0) return;
            if (!mainPool) mainPool = new GameObject(name + "_MainPool").transform;
            foreach (ObjectToPool op in objectsToPool)
                for (int i = 0; i < op.nbr; i++)
                {
                    GameObject o = Instantiate(op.o, mainPool);
                    o.name = op.o.name;
                    AddToPool(o);
                }
        }

        public GameObject RequireObject(string prefabName)
        {
            if (pooledObjects == null) Init();
            if (pooledObjects.ContainsKey(prefabName))
            {
                if (pooledObjects[prefabName].Count > 0)
                {
                    GameObject o = pooledObjects[prefabName][pooledObjects[prefabName].Count - 1];
                    pooledObjects[prefabName].RemoveAt(pooledObjects[prefabName].Count - 1);
                    o.SetActive(true);
                    return o;
                }
            }
            return CreateNew(prefabName);
        }

        //public GameObject RequireRandomObject()
        //{
        //    if (objectsToPool == null) Init();

        //    int _stop = Random.Range(0, objectsToPool.Length);
        //    int _i = 0;
        //    foreach (ObjectToPool item in objectsToPool)
        //    {
        //        if (_i != _stop)
        //            _i++;
        //        else
        //            return RequireObject(item.o.name);
        //    }
        //    return null;

        //}

        public void ReturnObject(GameObject o)
        {
            if (pooledObjects == null) Init();
            foreach (ObjectToPool op in objectsToPool)
            {
                if (op.o.name != o.name) continue;
                AddToPool(o);
                return;
            }
            return;

        }

        private GameObject CreateNew(string n)
        {
            //Debug.Log("Create New " + n);
            foreach (ObjectToPool op in objectsToPool)
                if (op.o.name == n)
                {
                    GameObject o = Instantiate(op.o, mainPool);
                    o.name = n;
                    return o;
                }
            return null;
        }

        private void AddToPool(GameObject o)
        {
            o.transform.SetParent(mainPool);
            if (pooledObjects.ContainsKey(o.name)) pooledObjects[o.name].Add(o);
            else
            {
                pooledObjects.Add(o.name, new List<GameObject>());
                pooledObjects[o.name].Add(o);
            }
            o.SetActive(false);
        }

        [System.Serializable]
        public struct ObjectToPool
        {
            public GameObject o;
            public int nbr;
        }
    }
}
