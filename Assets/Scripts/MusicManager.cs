using UnityEngine;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    // Singleton instance makes this accessible from anywhere
    public static MusicManager Instance { get; private set; }

    [Header("Audio Components")]
    [SerializeField] private AudioSource musicSource;

    // A custom class to easily map names to audio clips in the Inspector
    [System.Serializable]
    public class MusicTrack
    {
        public string trackName;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f;
    }

    [Header("Track Library")]
    public List<MusicTrack> tracks = new List<MusicTrack>();

    private void Awake() 
    {
        // 1. Singleton pattern: Destroy duplicates if a new scene loads
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        // 2. Keep the music manager alive across all scenes
        DontDestroyOnLoad(gameObject); 

        // 3. Auto-setup an AudioSource if you forgot to assign one
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.playOnAwake = false;
        }
    }

    public void PlayMusic(string trackName)
    {
        foreach (MusicTrack track in tracks)
        {
            if (track.trackName == trackName)
            {
                // Don't restart the track if it's already playing
                if (musicSource.clip == track.clip) return;

                musicSource.clip = track.clip;
                musicSource.volume = track.volume;
                musicSource.Play();
                return;
            }
        }
        
        Debug.LogWarning($"MusicManager: Track '{trackName}' not found in the library.");
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}