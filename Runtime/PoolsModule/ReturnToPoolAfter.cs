using System.Collections;
using UnityEngine;
#pragma warning disable CS0649
namespace Arkham.Onigiri.PoolsModule
{
    public class ReturnToPoolAfter : MonoBehaviour
    {
        [SerializeField] private PoolManager manager;
        [SerializeField] private float delay;

        private void OnEnable()
        {
            StartCoroutine(Delay());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(delay);
            ReturnToPool();
        }

        public void ReturnToPool()
        {
            manager.ReturnObject(gameObject);
        }
    }
}
