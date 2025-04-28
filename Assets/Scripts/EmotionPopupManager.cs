using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmotionPopupManager : MonoBehaviour
{
    public GameObject popupPanel;
    public SimpleDiary diaryScript;
    

    private string selectedEmotion = "";
    public Image emotionDisplayImage;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OpenPopup()
    {
        popupPanel.SetActive(true);
        selectedEmotion = ""; //√ ±‚»≠
    }
    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }
    public void SaveButton()
    {
        diaryScript.SaveWithEmotion(selectedEmotion);
        popupPanel.SetActive(false);
    }
    public void SelectEmotion(string emotion)
    {
        selectedEmotion = emotion;

        //diaryScript.SaveWithEmotion(selectedEmotion);

       // popupPanel.SetActive(false);

    }
}
