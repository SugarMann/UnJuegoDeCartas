using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Required for DOTween

[RequireComponent(typeof(Image))]
public class SceneFadeOut : MonoBehaviour
{
    [Header("Visual Configuration")] [Tooltip("Time in seconds to fade to black.")] [SerializeField]
    private float fadeDuration = 1.0f;

    private Image fadePanel;

    private void Awake()
    {
        fadePanel = GetComponent<Image>();

        // 1. Initial State: Invisible (Alpha 0)
        var c = fadePanel.color;
        c.a = 0f;
        fadePanel.color = c;

        // Important: Disable raycast so we can click buttons through it while invisible
        fadePanel.raycastTarget = false;
    }

    // Public method to start the effect. 
    // We pass a 'System.Action' (a function) to run ONLY when the fade finishes.
    public void StartFadeOut(Action onAnimationComplete)
    {
        // 2. Enable raycast to block clicks during transition
        fadePanel.raycastTarget = true;

        // 3. Animate to Black (Alpha 1)
        fadePanel.DOFade(1f, fadeDuration)
            .OnComplete(() =>
            {
                // Execute whatever logic was passed (e.g., LoadScene)
                onAnimationComplete?.Invoke();
            });
    }
}