using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    [SerializeField] private Audio audio;
    [Header("Audio Clips")]
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip testSound;

    [Header("Game Sounds")]
    [SerializeField] private List<AudioClip> collectionSounds;
    [SerializeField] private AudioClip bigWinSound;

    public void Add_ClickSound(){
        float volume = SharedData.volumes[1] * SharedData.masterVolume;
        audio.Play(clickSound, volume);
    }

    public void Play_TestSound(float volume){
        audio.Play(testSound, volume);
    }

    public void Play_CollectSound(){
        Debug.Log("helloooo?");
        float bVol = 1f;
        if(SharedData.volumes != null)
            bVol = SharedData.volumes[1];

        float volume = bVol * SharedData.masterVolume;

        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        AudioClip soundClip = null;
        if (currentSceneIdx%4!=0) //This needs to be checked once we have final scene
            soundClip = collectionSounds[UnityEngine.Random.Range(0, collectionSounds.Count)];
        else
            soundClip = bigWinSound;
            

        audio.PlayForce(soundClip, volume);
    }

    public void Play_WinShow(){
        float volume = SharedData.volumes[1] * SharedData.masterVolume;
        audio.Play(testSound, volume);
    }
}
