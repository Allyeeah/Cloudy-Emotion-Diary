using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EmotionSelector : MonoBehaviour
{
    public Image emotionImageDisplay;
    public GameObject popupPanel;
    public GameObject savedDiaryPanel;
    // Start is called before the first frame update
    void Start()
    {
        emotionImageDisplay.gameObject.SetActive(false); // 비활성화
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectEmotionFromButton(Button btn)
    {
        // 버튼 안에 있는 이미지(Sprite)를 가져오기
        Image btnImage = btn.GetComponent<Image>();
        Sprite selectedSprite = btnImage.sprite;

        // 이미지 표시
        emotionImageDisplay.sprite = selectedSprite;
        emotionImageDisplay.color = Color.white; // 완전 보이게
        emotionImageDisplay.gameObject.SetActive(true); // 혹시 비활성 상태였다면

        // 감정 저장 (원하면)
        string date = CalendarController.selectedDate;
        PlayerPrefs.SetString("emotion_" + date, selectedSprite.name);
        PlayerPrefs.Save();

        // 달력 새로고침해서 감정 이모지 표시!
        CalendarController._calendarInstance.CreateCalendar();

        popupPanel.SetActive(false); // 팝업 닫기
        
    }
}
