using UnityEngine;

public class SceneMusicChanger : MonoBehaviour
{
    [Header("Music Configuration")] [Tooltip("The music track that should play in this scene.")] [SerializeField]
    private AudioClip sceneMusic;

    private void Start()
    {
        // Find the persistent Music Player and tell it to switch tracks
        if (BackgroundMusic.Instance != null) BackgroundMusic.Instance.PlayMusic(sceneMusic);
    }
}