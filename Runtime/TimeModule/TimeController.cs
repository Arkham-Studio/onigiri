using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class TimeController : MonoBehaviour
{
    [SerializeField] private TimeManager m_TimeManager;
    [SerializeField] private bool isAutoUpdate = true;
    [SerializeField] private TimePack[] packs;

    private void Start()
    {
        if (!isAutoUpdate)
            RefreshTime();
    }

    private void OnEnable()
    {
        if (m_TimeManager.enabledTime != null) m_TimeManager.enabledTime.SetValue(Time.time);
    }

    private void Update()
    {
        if (isAutoUpdate)
            RefreshTime();

        foreach (TimePack item in packs)
            if (Time.frameCount % item.frameStep == 0) item.response.Invoke();
    }

    public void RefreshTime()
    {
        if (m_TimeManager.date != null) m_TimeManager.date.SetValue(System.DateTime.Now.ToString("s"));
        if (m_TimeManager.longDate != null) m_TimeManager.longDate.SetValue(System.DateTime.Now.Ticks);

        if (m_TimeManager.frameCount != null) m_TimeManager.frameCount.SetValue(Time.frameCount);
        if (m_TimeManager.time != null) m_TimeManager.time.SetValue(Time.time);
    }

    [System.Serializable]
    public class TimePack
    {
#if UNITY_EDITOR
        [TextArea(2, 4), HideLabel()]
        public string infos;
#endif
        public float frameStep;
        public UnityEvent response;
    }

}
