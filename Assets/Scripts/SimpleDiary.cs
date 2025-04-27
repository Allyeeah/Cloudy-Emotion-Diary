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
        // 처음엔 안 보이게
        if (photoDisplay != null)
            photoDisplay.color = new Color(0, 0, 0, 0);
    }

    public void SaveWithEmotion(string emotion)
    {
        string date = CalendarController.selectedDate;
        string content = inputField.text;


        PlayerPrefs.SetString("diary_" + date, content); // 일기 내용 저장
        PlayerPrefs.SetString("photo_" + date, photoPath);  // 사진 경로도 저장
        PlayerPrefs.SetString("emotion_" + date, emotion);  // 감정 저장

        PlayerPrefs.Save();

        LoadText(date); //화면에 즉시 반영
        Debug.Log("저장됨 날짜: " + date);
        Debug.Log("내용: " + content);
        Debug.Log("사진 경로: " + photoPath);
        Debug.Log("감정: " + emotion);
    }

    public void LoadText(string date)
    {
        string content = PlayerPrefs.GetString("diary_" + date, "일기 없음");
        string savedPhoto = PlayerPrefs.GetString("photo_" + date, "");

        displayText.text = content;

        // 사진 불러오기
        if (!string.IsNullOrEmpty(savedPhoto) && File.Exists(savedPhoto))
        {
            byte[] imgBytes = File.ReadAllBytes(savedPhoto);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);
            photoDisplay.texture = tex;
            // 사진 선택됐으니 다시 보이게!
            photoDisplay.color = Color.white;
        }
    }


    // 에디터 전용: PC에서 사진 선택
    public void SelectPhoto()
    {
#if UNITY_EDITOR
        string path = UnityEditor.EditorUtility.OpenFilePanel("이미지 선택", "", "png,jpg,jpeg");
        if (!string.IsNullOrEmpty(path))
        {
            photoPath = path;

            byte[] imgBytes = File.ReadAllBytes(path);
            Texture2D tex = new Texture2D(2, 2);
            tex.LoadImage(imgBytes);
            photoDisplay.texture = tex;
           // photoDisplay.color = new Color(1, 1, 1, 0.3f); // 살짝 흐리게 표시해도 좋음 (선택만 된 상태 표시용)

        }
#endif
    }

}
