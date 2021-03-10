using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame() 
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettings() 
    {
        SceneManager.LoadScene(2);
    }

    public void QuitTheGame()
    {
        Application.Quit();
    }
}
