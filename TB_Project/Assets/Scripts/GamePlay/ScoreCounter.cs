using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Text scoreView;
    private static int score = 0;

    private void Update()
    {
        UpdateScoreView();
    }

    private void UpdateScoreView()
    {
        scoreView.text = "Score: " + score;
    }

    public static void IncreaseScore(int points) 
    {
        score += points;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        score = 0;
    }
}
