using UnityEngine;
using System.Collections.Generic;

public class AudioController : MonoBehaviour
{
    [SerializeField] private Audio audio;
    [Header("Audio Clips")]
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip testSound;

    [Header("Game Sounds")]
    [SerializeField] private AudioClip wallHitSound;
    [SerializeField] private AudioClip walkingSound;
    [SerializeField] private List<AudioClip> collectionSounds;
    [SerializeField] private AudioClip bigWinSound;

    public void Add_ClickSound(){
        float volume = SharedData.volumes[1] * SharedData.masterVolume;
        audio.Play(clickSound, volume);
    }

    public void Play_TestSound(float volume){
        audio.Play(testSound, volume);
    }
    
    public void Play_HitSound(){
        float volume = SharedData.volumes[1] * SharedData.masterVolume;
        audio.Play(testSound, volume);
    }

    public void Play_CollectSound(){
        float volume = SharedData.volumes[1] * SharedData.masterVolume;
        audio.Play(testSound, volume);
    }

    public void Play_WinShow(){
        float volume = SharedData.volumes[1] * SharedData.masterVolume;
        audio.Play(testSound, volume);
    }
}
