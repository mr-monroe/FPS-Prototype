using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuPanel;

    private bool isOpened;

    private void Awake()
    {
        isOpened = false;
    }

    public void OnPausePressed() 
    {
        if (isOpened) Resume();
        else PauseGame();
        isOpened = !isOpened;
    }

    public void PauseGame() 
    {
        Time.timeScale = 0;
        PauseMenuPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume() 
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked; 
    }

    public void RestartGame() 
    {
        Time.timeScale = 1;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void LoadMainMenu() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0); 
    }
}
