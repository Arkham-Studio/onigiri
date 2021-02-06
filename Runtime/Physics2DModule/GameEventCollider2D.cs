using UnityEngine;

public class GameEventCollider2D : MonoBehaviour
{

    public bool isTrigger = false;
    public ColliderEventPack[] enterPack;
    public ColliderEventPack[] exitPack;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isTrigger) return;
        foreach (ColliderEventPack item in enterPack)
            if (item.layer == (item.layer | (1 << collision.gameObject.layer)))
            {
                if (item.unityEvent != null) item.unityEvent.Invoke(collision.gameObject);
            }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (isTrigger) return;
        foreach (ColliderEventPack item in exitPack)
            if (item.layer == (item.layer | (1 << collision.gameObject.layer)))
            {
                if (item.unityEvent != null) item.unityEvent.Invoke(collision.gameObject);
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTrigger) return;
        foreach (ColliderEventPack item in enterPack)
            if (item.layer == (item.layer | (1 << collision.gameObject.layer)))
            {
                if (item.unityEvent != null) item.unityEvent.Invoke(collision.gameObject);
            }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isTrigger) return;
        foreach (ColliderEventPack item in exitPack)
            if (item.layer == (item.layer | (1 << collision.gameObject.layer)))
            {
                if (item.unityEvent != null) item.unityEvent.Invoke(collision.gameObject);
            }
    }

    [System.Serializable]
    public class ColliderEventPack
    {
        public LayerMask layer;
        public GameObjectUnityEvent unityEvent;
        //public UnityEvent<GameObject> unityEvent;
    }
}
