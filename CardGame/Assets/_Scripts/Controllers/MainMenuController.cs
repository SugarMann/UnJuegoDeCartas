using UnityEngine;
using UnityEngine.SceneManagement;

// Required to change scenes

// Required if we manipulate UI elements by code

public class MainMenuController : MonoBehaviour
{
    [Header("Configuration")] [Tooltip("The exact name of the game scene")] [SerializeField]
    private string gameSceneName = "MapScene";

    [Tooltip("Reference to the Settings Panel")] [SerializeField]
    private GameObject settingsPanel;

    [Tooltip("Reference to the Main Buttons Panel")] [SerializeField]
    private GameObject mainButtonsPanel;

    // --- BUTTON FUNCTIONS ---

    // Function to start the game
    public void PlayGame()
    {
        Debug.Log("Loading Scene: " + gameSceneName);
        SceneManager.LoadScene(gameSceneName);
    }

    // Function to quit the application
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    // Function to toggle the Settings Panel (Open/Close)
    public void ToggleSettings(bool isOpen)
    {
        if (settingsPanel != null) settingsPanel.SetActive(isOpen);
        if (mainButtonsPanel != null) mainButtonsPanel.SetActive(!isOpen);
    }

    // --- AUDIO FUNCTIONS ---

    // Link this to a Slider's "OnValueChanged" event
    public void SetMasterVolume(float volume)
    {
        // AudioListener.volume controls global volume (0.0 to 1.0)
        AudioListener.volume = volume;
    }
}