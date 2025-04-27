using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CalendarDateItem : MonoBehaviour
{
    public void OnDateItemClick()
    {
        string day = gameObject.GetComponentInChildren<Text>().text;
        CalendarController._calendarInstance.OnDateItemClick(day);
    }
}

