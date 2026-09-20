
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Audio : MonoBehaviour
{
    // EDITOR
    [SerializeField] private bool stackAudio = false;

    [Header("Walk Sound")]
    [SerializeField] private float walkSoundInterval = 0.4f;

    // CODE
    private float customVolume = 1.0f;
    [HideInInspector] public bool isParent = true;

    private float walkTimer = 0f;

    // COMPONENTS
    private AudioSource audioSource;

    public void PlayForce(AudioClip clip, float volume = -1.0f)
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (clip == null)
        {
            Debug.LogWarning("No AudioClip was provided.");
            return;
        }

        if (volume == -1.0f)
            volume = customVolume;

        if (stackAudio && isParent)
        {
            Audio newAudioSource = Instantiate(
                this,
                transform.position,
                transform.rotation
            );

            newAudioSource.customVolume = volume;
            newAudioSource.isParent = false;
            newAudioSource.PlayForce(clip, volume);
        }
        else
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
        }
    }

    public void Play(AudioClip clip, float volume = -1.0f)
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (clip == null)
        {
            Debug.LogWarning("No AudioClip was provided.");
            return;
        }

        if (volume == -1.0f)
            volume = customVolume;

        if (stackAudio && isParent)
        {
            Audio newAudioSource = Instantiate(
                this,
                transform.position,
                transform.rotation
            );

            newAudioSource.customVolume = volume;
            newAudioSource.isParent = false;
            newAudioSource.Play(clip, volume);
        }
        else if (!audioSource.isPlaying)
        {
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
        }
    }

    // WALK SOUND
    public void PlayWalk(AudioClip walkClip, float volume = -1.0f)
    {
        walkTimer -= Time.deltaTime;

        if (walkTimer > 0f)
            return;

        if (walkClip == null)
        {
            Debug.LogWarning("No walk AudioClip was provided.");
            return;
        }

        // Reset timer
        walkTimer = walkSoundInterval;

        // Play footstep
        Play(walkClip, volume);
    }

    public void ResetWalkTimer()
    {
        walkTimer = 0f;
    }

    public void SetVolume(float volume)
    {
        customVolume = volume;

        if (audioSource != null)
            audioSource.volume = volume;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!isParent && !audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}