using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Singleton instance to access this script from anywhere
    public static SceneController Instance;

    private void Awake()
    {
        // Ensure only one instance exists and persists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Call this method to switch to the Combat Scene
    public void LoadCombat()
    {
        Debug.Log("Loading Combat Scene...");
        // You might want to save the current map state here in the future
        SceneManager.LoadScene("CombatScene");
    }

    // Call this method to return to the Map
    public void ReturnToMap()
    {
        Debug.Log("Returning to Map Scene...");
        SceneManager.LoadScene("MapScene");
    }

	// Call this method to switch to Game Over Scene
	public void LoadGameOver()
    {
        Debug.Log("Loading Game Over Screen...");
        SceneManager.LoadScene("GameOverScene");
    }
}