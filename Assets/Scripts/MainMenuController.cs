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
        SceneManager.LoadScene("ARScene"); // ARScene���� �̵�
    }
    public void GoToDiaryScene()
    {
        SceneManager.LoadScene("DiaryScene"); // DiaryScene���� �̵�
    }
}
