using UnityEngine;

public class ExposeTime : MonoBehaviour
{
    public StringVariable date;
    public LongVariable longDate;

    private void Start()
    {
        RefreshTime();
    }


    public void RefreshTime()
    {
        if (date != null) date.SetValue(System.DateTime.Now.ToString("s"));
        if (longDate != null) longDate.SetValue(System.DateTime.Now.Ticks);
    }
}
