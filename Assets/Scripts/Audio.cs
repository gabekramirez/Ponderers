using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    // EDITOR
    public SharedData.GameAudioType audioType = SharedData.GameAudioType.SoundEffects;
    [SerializeField] private bool stackAudio = false;

    // CODE
    private float customVolume = 1.0f;
    [HideInInspector] public bool isParent = true;

    // COMPONENTS
    AudioSource audioSource;

    public void play(float volume = -1.0f)
    {
        if (volume == -1.0f) {volume = customVolume;}
        if (stackAudio && isParent)
        {
            Audio newAudioSource = Instantiate(this);
            newAudioSource.customVolume = volume;
            newAudioSource.isParent = false;
            newAudioSource.play(volume);
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void setVolume(float volume)
    {
        customVolume = volume;
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.volume = customVolume * SharedData.volumes[(int)audioType];
        if (!isParent && !audioSource.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
