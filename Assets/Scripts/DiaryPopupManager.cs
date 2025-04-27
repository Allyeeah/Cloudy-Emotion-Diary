using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class DiaryPopupManager : MonoBehaviour
{
    public static DiaryPopupManager Instance;

    public GameObject popupPanel;
    public Text dateText, contentText;
    public Image emotionImage;
    public RawImage photoDisplay;

    public Sprite happySprite, sadSprite, angrySprite;

    private void Awake()
    {
        Instance = this;
    }
    public void ShowDiary(string date)
    {
        // 감정이 저장되어 있는지 확인 (none이면 무시)
        string emotion = PlayerPrefs.GetString("emotion_" + date, "none");
        if (emotion == "none")
        {
            Debug.Log("감정이 없는 날이므로 팝업을 열지 않음: " + date);
            return;
        }


        Debug.Log("ShowDiary 호출됨: " + date);
        // 날짜 표시
        dateText.text = date;

        // 내용
        string content = PlayerPrefs.GetString("diary_" + date, "일기 없음");
        contentText.text = content;

        // 감정 이모티콘 표시
        emotionImage.sprite = GetEmotionSprite(emotion);
        emotionImage.color = (emotionImage.sprite != null) ? Color.white : new Color(1, 1, 1, 0);

        // 사진
        string photoPath = PlayerPrefs.GetString("photo_" + date, "");
        if (!string.IsNullOrEmpty(photoPath) && File.Exists(photoPath))
        {
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(File.ReadAllBytes(photoPath));
            photoDisplay.texture = tex;
            photoDisplay.color = Color.white;
        }
        else
        {
            photoDisplay.color = new Color(1, 1, 1, 0);
        }

        popupPanel.SetActive(true); // 팝업 열기
    }

    Sprite GetEmotionSprite(string emotion)
    {
        switch (emotion)
        {
            case "happy": return happySprite;
            case "sad": return sadSprite;
            case "angry": return angrySprite;
            default: return null;
        }
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
}
