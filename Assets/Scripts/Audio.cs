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
    private AudioSource audioSource;

    public void Play(AudioClip clip, float volume = -1.0f)
    {
        if(audioSource == null)
            audioSource = this.gameObject.transform.GetComponent<AudioSource>();

        if (clip == null)
        {
            Debug.LogWarning("No AudioClip was provided.");
            return;
        }

        if (volume == -1.0f)
            volume = customVolume;

        if (stackAudio && isParent)
        {
            Audio newAudioSource = Instantiate(this, transform.position, transform.rotation);

            newAudioSource.customVolume = volume;
            newAudioSource.isParent = false;
            newAudioSource.Play(clip, volume);
        }
        else if (!audioSource.isPlaying)
        {

            Debug.Log(volume);
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
        }
    }

    public void SetVolume(float volume)
    {
        customVolume = volume;
        audioSource.volume = volume;
    }

    private void Awake()
    {
        audioSource = this.gameObject.transform.GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!isParent && !audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}