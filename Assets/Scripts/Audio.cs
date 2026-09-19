using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    // EDITOR
    [SerializeField] private SharedData.GameAudioType audioType;

    // CODE
    private float customVolume = 1.0f;
    [HideInInspector] public bool isParent = true;

    // COMPONENTS
    AudioSource audioSource;

    public void play(float volume = -1.0f, bool stackAudio = true)
    {
        if (volume == -1.0f) {volume = customVolume;}
        if (stackAudio)
        {
            Audio newAudioSource = Instantiate(this);
            newAudioSource.customVolume = volume;
            newAudioSource.isParent = false;
            newAudioSource.play(volume, false);
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
        audioSource.volume = customVolume * SharedData.Instance.volumes[(int)audioType];
        if (!isParent && !audioSource.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
