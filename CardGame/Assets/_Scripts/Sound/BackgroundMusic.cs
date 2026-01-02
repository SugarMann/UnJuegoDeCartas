using UnityEngine;

public class BackgroundMusic : PersistentSingleton<BackgroundMusic>
{
    private AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake(); // Keep your Singleton logic
        _audioSource = GetComponent<AudioSource>();

        // Optional: Ensure loop is on
        if (_audioSource != null) _audioSource.loop = true;
    }

    public void PlayMusic(AudioClip musicClip)
    {
        // Safety check
        if (_audioSource == null || musicClip == null) return;

        // Optimization: If the requested song is ALREADY playing, do nothing.
        // This prevents the music from restarting if you go Scene A -> Scene A.
        if (_audioSource.clip == musicClip) return;

        // Switch the track
        _audioSource.Stop();
        _audioSource.clip = musicClip;
        _audioSource.Play();
    }
}