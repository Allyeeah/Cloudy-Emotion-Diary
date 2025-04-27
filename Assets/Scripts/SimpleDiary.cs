using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class SimpleDiary : MonoBehaviour
{
    public TMP_InputField inputField;
    public Text displayText;
    public RawImage photoDisplay;

    private string today;
    private string photoPath = "";

    void Start()
    {
        today = System.DateTime.Now.ToString("yy.M.d");
        // ó���� �� ���̰�
        if (photoDisplay != null)
            photoDisplay.color = new Color(0, 0, 0, 0);
    }

    public void SaveWithEmotion(string emotion)
    {
        string date = CalendarController.selectedDate;
        string content = inputField.text;


        PlayerPrefs.SetString("diary_" + date, content); // �ϱ� ���� ����
        PlayerPrefs.SetString("photo_" + date, photoPath);  // ���� ��ε� ����
        PlayerPrefs.SetString("emotion_" + date, emotion);  // ���� ����

        PlayerPrefs.Save();

        LoadText(date); //ȭ�鿡 ��� �ݿ�
        Debug.Log("����� ��¥: " + date);
        Debug.Log("����: " + content);
        Debug.Log("���� ���: " + photoPath);
        Debug.Log("����: " + emotion);
    }

    public void LoadText(string date)
    {
        string content = PlayerPrefs.GetString("diary_" + date, "�ϱ� ����");
        string savedPhoto = PlayerPrefs.GetString("photo_" + date, "");

        displayText.text = content;

        // ���� �ҷ�����
        if (!string.IsNullOrEmpty(savedPhoto) && File.Exists(savedPhoto))
        {
            byte[] imgBytes = File.ReadAllBytes(savedPhoto);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);
            photoDisplay.texture = tex;
            // ���� ���õ����� �ٽ� ���̰�!
            photoDisplay.color = Color.white;
        }
    }


    // ������ ����: PC���� ���� ����
    public void SelectPhoto()
    {
#if UNITY_EDITOR
        string path = UnityEditor.EditorUtility.OpenFilePanel("�̹��� ����", "", "png,jpg,jpeg");
        if (!string.IsNullOrEmpty(path))
        {
            photoPath = path;

            byte[] imgBytes = File.ReadAllBytes(path);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);
            photoDisplay.texture = tex;
           // photoDisplay.color = new Color(1, 1, 1, 0.3f); // ��¦ �帮�� ǥ���ص� ���� (���ø� �� ���� ǥ�ÿ�)

        }
#endif
    }

}
