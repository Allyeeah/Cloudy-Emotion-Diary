using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToARScene()
    {
        SceneManager.LoadScene("ARScene"); // ARScene으로 이동
    }
    public void GoToDiaryScene()
    {
        SceneManager.LoadScene("DiaryScene"); // DiaryScene으로 이동
    }
}
