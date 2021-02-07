using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Arkham.Onigiri.PoolsModule
{
    [InlineEditor(InlineEditorObjectFieldModes.Foldout, DrawHeader = false)]
    public class BasePoolManager<T> : ScriptableObject where T : Component
    {
        public int nbrToInit;
        public T obj;

        private Transform mainPoolTransform;
        private List<T> pool;

        public void Init()
        {
            pool = new List<T>();

            if (!mainPoolTransform) mainPoolTransform = new GameObject(name + "_MainPool").transform;

            for (int i = 0; i < nbrToInit; i++)
            {
                T o = Instantiate(obj, mainPoolTransform);
                AddToPool(o);
            }
        }

        public T RequireObject()
        {
            if (pool == null || mainPoolTransform == null) Init();

            if (pool.Count > 0)
            {
                T o = pool[pool.Count - 1];
                pool.RemoveAt(pool.Count - 1);
                o.gameObject.SetActive(true);
                return o;
            }
            return CreateNew();
        }


        public void ReturnObject(T o)
        {
            if (pool == null) Init();

            AddToPool(o);
            return;

        }

        public bool ReturnObject(GameObject o)
        {
            T t = o.GetComponent<T>();
            if (t == null) return false;
            else ReturnObject(t);
            return true;
        }

        private T CreateNew()
        {

            T o = Instantiate(obj, mainPoolTransform);
            return o;

        }

        private void AddToPool(T o)
        {
            o.transform.SetParent(mainPoolTransform);
            pool.Add(o);
            o.gameObject.SetActive(false);
        }

    }
}
