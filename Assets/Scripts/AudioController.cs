using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private Audio audio;
    [Header("Audio Clips")]
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip testSound;

    public void Add_ClickSound(){
        float volume = SharedData.volumes[(int)audio.audioType] * SharedData.masterVolume;
        audio.Play(clickSound, volume);
    }

    public void Play_TestSound(float volume){
        audio.Play(testSound, volume);
    }
}
