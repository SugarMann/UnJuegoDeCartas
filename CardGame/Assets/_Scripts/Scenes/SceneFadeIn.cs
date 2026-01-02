using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
// Required for Image

// Required for DOTween

// Add this script directly to the Black Panel object
[RequireComponent(typeof(Image))]
public class SceneFadeIn : MonoBehaviour
{
    [Header("Configuration")] [Tooltip("How long it takes to reveal the scene.")] [SerializeField]
    private float fadeDuration = 1.0f;

    private Image fadePanel;

    private void Awake()
    {
        // Get the Image component attached to this GameObject
        fadePanel = GetComponent<Image>();

        // 1. INSTANT SETUP: Ensure the panel starts fully BLACK and OPAQUE.
        // This runs before the first frame is rendered to avoid flashes.
        var c = fadePanel.color;
        c.a = 1f; // Alpha 1 = Opaque
        fadePanel.color = c;

        // Ensure it blocks mouse clicks while black
        fadePanel.raycastTarget = true;
    }

    private void Start()
    {
        // 2. THE ANIMATION: Fade Alpha from 1 down to 0.
        fadePanel.DOFade(0f, fadeDuration)
            .SetEase(Ease.Linear) // Smooth, even fade
            .OnComplete(OnFadeFinished); // Call this function when done
    }

    // This function runs automatically when the tween finishes
    private void OnFadeFinished()
    {
        // CRITICAL STEP:
        // Deactivate the panel. If we don't do this, it will remain
        // as an invisible layer blocking clicks on menu buttons.
        gameObject.SetActive(false);
    }
}