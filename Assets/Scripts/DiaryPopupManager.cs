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
        // ������ ����Ǿ� �ִ��� Ȯ�� (none�̸� ����)
        string emotion = PlayerPrefs.GetString("emotion_" + date, "none");
        if (emotion == "none")
        {
            Debug.Log("������ ���� ���̹Ƿ� �˾��� ���� ����: " + date);
            return;
        }


        Debug.Log("ShowDiary ȣ���: " + date);
        // ��¥ ǥ��
        dateText.text = date;

        // ����
        string content = PlayerPrefs.GetString("diary_" + date, "�ϱ� ����");
        contentText.text = content;

        // ���� �̸�Ƽ�� ǥ��
        emotionImage.sprite = GetEmotionSprite(emotion);
        emotionImage.color = (emotionImage.sprite != null) ? Color.white : new Color(1, 1, 1, 0);

        // ����
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

        popupPanel.SetActive(true); // �˾� ����
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
