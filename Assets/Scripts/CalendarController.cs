using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CalendarController : MonoBehaviour
{
    public GameObject _calendarPanel;
    public Text _yearNumText;
    public Text _monthNumText;

    public GameObject _item;

    public List<GameObject> _dateItems = new List<GameObject>();
    const int _totalDateNum = 42;

    private DateTime _dateTime;
    public static CalendarController _calendarInstance;

    public static string selectedDate; //날짜 포맷: yyyy-MM-dd

    public Sprite happySprite;
    public Sprite sadSprite;
    public Sprite angrySprite;
    public Sprite noneSprite;

    void Start()
    {
        _calendarInstance = this;
        Vector3 startPos = _item.transform.localPosition;
        _dateItems.Clear();
        _dateItems.Add(_item);

        for (int i = 1; i < _totalDateNum; i++)
        {
            GameObject item = GameObject.Instantiate(_item) as GameObject;
            item.name = "Item" + (i + 1).ToString();
            item.transform.SetParent(_item.transform.parent);
            item.transform.localScale = Vector3.one;
            item.transform.localRotation = Quaternion.identity;
            item.transform.localPosition = new Vector3((i % 7) * 36  + startPos.x, startPos.y - (i / 7) * 30, startPos.z);

            _dateItems.Add(item);
        }

        _dateTime = DateTime.Now;

        CreateCalendar();

        _calendarPanel.SetActive(false);
    }

    public void CreateCalendar()
    {
        DateTime firstDay = _dateTime.AddDays(-(_dateTime.Day - 1));
        int index = GetDays(firstDay.DayOfWeek);

        int date = 0;
        for (int i = 0; i < _totalDateNum; i++)
        {
            Text label = _dateItems[i].GetComponentInChildren<Text>();
            _dateItems[i].SetActive(false);

            if (i >= index)
            {
                DateTime thatDay = firstDay.AddDays(date);
                if (thatDay.Month == firstDay.Month)
                {
                    _dateItems[i].SetActive(true);

                    label.text = (date + 1).ToString();


                    //감정 불러오기
                    string fullDate = thatDay.ToString("yyyy-MM-dd");
                    string emotion = PlayerPrefs.GetString("emotion_" + fullDate, "none");

                    // 감정 아이콘 오브젝트 찾기
                    Transform icon = _dateItems[i].transform.Find("EmotionIcon");
                    if (icon != null)
                    {
                        Image iconImage = icon.GetComponent<Image>();
                        Sprite emotionSprite = GetEmotionSprite(emotion);

                        if (emotion == "none" || emotionSprite == null)
                        {
                            // 감정이 없거나 정의되지 않았으면 아이콘 숨기기
                            iconImage.sprite = null;
                            iconImage.color = new Color(1, 1, 1, 0); // 완전 투명
                        }
                        else
                        {
                            // 감정 있으면 아이콘 표시
                            iconImage.sprite = emotionSprite;
                            iconImage.color = Color.white;

                            //  클릭 가능하게 만들기
                            Button btn = icon.GetComponent<Button>();
                            if (btn == null) btn = icon.gameObject.AddComponent<Button>();

       

                            btn.onClick.RemoveAllListeners(); // 중복 방지

                            // 클로저 문제 방지: 지역 변수 복사
                            string selectedDate = fullDate;

                            btn.onClick.AddListener(() => {
                                if (DiaryPopupManager.Instance != null)
                                    DiaryPopupManager.Instance.ShowDiary(selectedDate);
                                else
                                    Debug.LogWarning("❗ DiaryPopupManager.Instance is null");
                            });

                        }
                    }

                    date++;
                }
            }
        }
        _yearNumText.text = _dateTime.Year.ToString();
        _monthNumText.text = _dateTime.Month.ToString("D2");
    }

    int GetDays(DayOfWeek day)
    {
        switch (day)
        {
            case DayOfWeek.Monday: return 1;
            case DayOfWeek.Tuesday: return 2;
            case DayOfWeek.Wednesday: return 3;
            case DayOfWeek.Thursday: return 4;
            case DayOfWeek.Friday: return 5;
            case DayOfWeek.Saturday: return 6;
            case DayOfWeek.Sunday: return 0;
        }

        return 0;
    }
    public void YearPrev()
    {
        _dateTime = _dateTime.AddYears(-1);
        CreateCalendar();
    }

    public void YearNext()
    {
        _dateTime = _dateTime.AddYears(1);
        CreateCalendar();
    }

    public void MonthPrev()
    {
        _dateTime = _dateTime.AddMonths(-1);
        CreateCalendar();
    }

    public void MonthNext()
    {
        _dateTime = _dateTime.AddMonths(1);
        CreateCalendar();
    }

    public void ShowCalendar(Text target)
    {
        _calendarPanel.SetActive(true);
        _target = target;
        //_calendarPanel.transform.position = new Vector3(965, 475, 0);//Input.mousePosition-new Vector3(0,120,0);
    }

    Text _target;

    //Item 클릭했을 경우 Text에 표시.
    public void OnDateItemClick(string day)
    {
        selectedDate = _yearNumText.text + "-" + _monthNumText.text + "-" + int.Parse(day).ToString("D2");
        _target.text = selectedDate;
        _calendarPanel.SetActive(false);
    }


    Sprite GetEmotionSprite(string emotion)
    {
        switch (emotion)
        {
            case "happy": return happySprite;
            case "sad": return sadSprite;
            case "angry": return angrySprite;
            default: return noneSprite;
        }
    }


}
