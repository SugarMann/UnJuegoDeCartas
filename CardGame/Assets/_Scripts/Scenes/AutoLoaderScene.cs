using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAutoLoader : MonoBehaviour
{
    [Header("Configuration")] [SerializeField]
    private string sceneToLoad = "MenuScene";

    [SerializeField] private float autoLoadTime = 5f;

    [Header("References")]
    // Reference to the script we just created
    [SerializeField]
    private SceneFadeOut fadeOutEffect;

    private bool isTransitioning;

    private float timer;

    private void Update()
    {
        if (isTransitioning) return;

        timer += Time.deltaTime;

        // Trigger condition: Time passed OR Escape key pressed
        if (timer >= autoLoadTime || Input.GetKeyDown(KeyCode.Escape)) TriggerSequence();
    }

    private void TriggerSequence()
    {
        isTransitioning = true;

        if (fadeOutEffect != null)
            // Call the visual effect and tell it what to do when finished (LoadNextScene)
            fadeOutEffect.StartFadeOut(LoadNextScene);
        else
            // Fallback if no visual effect is assigned
            LoadNextScene();
    }

    // This function is passed as a parameter to the FadeOut script
    private void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}