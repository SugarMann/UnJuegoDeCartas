using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public Button restartButton;

    private void Start()
    {
        // Safety check
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartClicked);
        }
    }

    private void OnRestartClicked()
    {
        Debug.Log("Restarting Run...");
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetRun();
        }
        else
        {
            // Fallback if GameManager is missing (started scene directly)
            UnityEngine.SceneManagement.SceneManager.LoadScene("MapScene");
        }
    }
}