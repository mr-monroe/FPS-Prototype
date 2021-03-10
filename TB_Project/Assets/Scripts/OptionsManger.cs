using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsManger : MonoBehaviour
{
    [SerializeField] private Slider speedAdjustmentSlider;
    [SerializeField] private Text moveSpeedValueView;

    private float movespeed;

    private void Start()
    {
        //speedAdjustmentSlider.onValueChanged.AddListener(OnSliderValueChange);
    }
    public void CloseOptionsScreen() 
    {
        SceneManager.LoadScene(0);
    }

    private void Update()
    {
        moveSpeedValueView.text = "Player Movespeed: " + speedAdjustmentSlider.value;
    }

    public void SetOptions()
    {
        print("hello option");
        movespeed = speedAdjustmentSlider.value;
    }

    public void SaveSettings() 
    {
        PlayerData data = new PlayerData(movespeed);
        SaveSystem.SaveSettings(data);
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
        LoadMovementSettings();
    }

    private void LoadMovementSettings()
    {
        PlayerData loadedData = SaveSystem.LoadSettings();
        if (loadedData != null)
        { 
            movespeed = loadedData.movespeed;
            speedAdjustmentSlider.value = movespeed;
        } 
    }
}
