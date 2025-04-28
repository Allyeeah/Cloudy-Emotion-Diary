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
        emotionImageDisplay.gameObject.SetActive(false); // ��Ȱ��ȭ
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectEmotionFromButton(Button btn)
    {
        // ��ư �ȿ� �ִ� �̹���(Sprite)�� ��������
        Image btnImage = btn.GetComponent<Image>();
        Sprite selectedSprite = btnImage.sprite;

        // �̹��� ǥ��
        emotionImageDisplay.sprite = selectedSprite;
        emotionImageDisplay.color = Color.white; // ���� ���̰�
        emotionImageDisplay.gameObject.SetActive(true); // Ȥ�� ��Ȱ�� ���¿��ٸ�

        // ���� ���� (���ϸ�)
        string date = CalendarController.selectedDate;
        PlayerPrefs.SetString("emotion_" + date, selectedSprite.name);
        PlayerPrefs.Save();

        // �޷� ���ΰ�ħ�ؼ� ���� �̸��� ǥ��!
        CalendarController._calendarInstance.CreateCalendar();

        popupPanel.SetActive(false); // �˾� �ݱ�
        
    }
}
