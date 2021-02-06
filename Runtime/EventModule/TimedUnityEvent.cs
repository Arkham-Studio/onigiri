using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class TimedUnityEvent : MonoBehaviour
{

    [Title("SETTINGS")]
    [SerializeField] private float delay = 0f;
    [SerializeField] private float interval = 1f;
    private float nextTime;

    [Title("EVENT")]
    public UnityEvent eventOnTime;


    //  MONOS
    private void OnEnable() => nextTime = Time.time + delay;

    public void Update()
    {
        if (Time.time < nextTime) return;

        nextTime = Time.time + interval;
        eventOnTime.Invoke();
    }

}
