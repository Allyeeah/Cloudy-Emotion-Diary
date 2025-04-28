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

    public Text dateText;

    private string today;
    private string photoPath = "";

    public TMP_InputField diaryInputField; // 기존 InputField를 inspector에서 연결해줘야 함
    public GameObject savedDiaryPanel; // 저장된 일기 보여주는 패널
    public GameObject writeDiaryPanel; // 수정하는 InputField 있는 패널
    public Text ModifyDate;


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

        // 날짜도 출력
        dateText.text = date;

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

    // 수정 버튼 누를 때 호출
    public void EnterModifyMode()
    {
        string date = CalendarController.selectedDate;
        string content = PlayerPrefs.GetString("diary_" + date, ""); // 저장된 일기 불러오기


        Debug.Log("수정모드 들어감");
        Debug.Log("선택된 날짜: " + date);
        Debug.Log("PlayerPrefs에서 읽어온 내용: " + content);


        if (diaryInputField != null)
        {
            diaryInputField.text = content; // 수정 입력창에 기존 내용을 채워넣기
            
        }
        else
        {
            Debug.LogWarning("InputField가 연결되어 있지 않습니다.");
        }

        if (savedDiaryPanel != null)
            savedDiaryPanel.SetActive(false); // 보기용 패널 끄기

        if (writeDiaryPanel != null)
            writeDiaryPanel.SetActive(true); // 수정용 패널 켜기

        Debug.Log("수정 모드 진입: 날짜 " + date + ", 내용: " + content);
    }

}
