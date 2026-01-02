using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// Required for TextMeshPro

public class IntroController : MonoBehaviour
{
    [Header("Tutorial Data")] [Tooltip("Drag your screenshots here.")] [SerializeField]
    private Sprite[] slideImages;

    [Tooltip("Write the explanation for each screenshot here.")]
    [TextArea(3, 5)] // Gives you more space to type in Inspector
    [SerializeField]
    private string[] slideTexts;

    [Header("UI References")] [SerializeField]
    private Image displayImage; // The Image component showing the screenshot

    [SerializeField] private TextMeshProUGUI displayText; // The Text component
    [SerializeField] private Button nextButton;
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject startButton; // The button to start the game

    [Header("Navigation Config")] [Tooltip("Name of the scene to load when tutorial finishes.")] [SerializeField]
    private string nextSceneName = "MapScene";

    // Reference to your FadeOut script (Optional, leave empty if not using fades here)
    [SerializeField] private SceneFadeOut fadeOutEffect;

    private int currentIndex;

    private void Start()
    {
        // Initialize the first slide
        UpdateSlideUI();
    }

    // Call this from the "Next" button
    public void NextSlide()
    {
        if (currentIndex < slideImages.Length - 1)
        {
            currentIndex++;
            UpdateSlideUI();
        }
    }

    // Call this from the "Back" button
    public void PreviousSlide()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateSlideUI();
        }
    }

    // Call this from the "Start Game" button
    public void StartGame()
    {
        if (fadeOutEffect != null)
            fadeOutEffect.StartFadeOut(() => SceneManager.LoadScene(nextSceneName));
        else
            SceneManager.LoadScene(nextSceneName);
    }

    private void UpdateSlideUI()
    {
        // 1. Update visual content
        if (slideImages.Length > currentIndex)
            displayImage.sprite = slideImages[currentIndex];

        if (slideTexts.Length > currentIndex)
            displayText.text = slideTexts[currentIndex];

        // 2. Button Logic
        // Hide "Back" if we are on the first slide (Index 0)
        backButton.gameObject.SetActive(currentIndex > 0);

        // Hide "Next" if we are on the last slide
        var isLastSlide = currentIndex == slideImages.Length - 1;
        nextButton.gameObject.SetActive(!isLastSlide);

        // Show "Start" ONLY if we are on the last slide
        startButton.SetActive(isLastSlide);
    }
}