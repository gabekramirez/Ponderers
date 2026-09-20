using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    private Audio audio;
    [Header("Audio Clips")]
    private AudioClip clickSound;
    private AudioClip testSound;

    [Header("Game Sounds")]
    private List<AudioClip> collectionSounds;
    private AudioClip bigWinSound;
    private GameObject audioPrefab;

    void Awake(){
        //Special case for MainMenu
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIdx==0){
            audioPrefab = Resources.Load<GameObject>("Audio");
            audio = GameObject.Instantiate(audioPrefab).transform.GetComponent<Audio>();
        }
        InitializeSounds();
    }

    public void RanAwake(){
        audio = GameObject.Find("Audio(Clone)").transform.GetComponent<Audio>();
        
        InitializeSounds();

        GameObject.Find("Managers/LevelManager").transform.GetComponent<LevelManager>().audioController = this;
    }

    void InitializeSounds(){
        bigWinSound = Resources.Load<AudioClip>("SFX/win");
        clickSound = Resources.Load<AudioClip>("SFX/click");
        testSound = Resources.Load<AudioClip>("SFX/collect1");

        collectionSounds = new List<AudioClip>();
        // Make sure the capitalization matches your actual file names perfectly
        collectionSounds.Add(Resources.Load<AudioClip>("SFX/Collect1")); 
        collectionSounds.Add(Resources.Load<AudioClip>("SFX/collect2"));
        collectionSounds.Add(Resources.Load<AudioClip>("SFX/collect3"));
    }

    public void Add_ClickSound(){
        float volume = SharedData.volumes[1] * SharedData.masterVolume;
        Debug.Log(clickSound);
        Debug.Log(volume);
        audio.PlayForce(clickSound, volume);
    }

    public void Play_TestSound(float volume){
        Debug.Log(testSound);
        audio.Play(testSound, volume);
    }

    public void Play_CollectSound(){
        float bVol = 1f;
        if(SharedData.volumes != null)
            bVol = SharedData.volumes[1];

        float volume = bVol * SharedData.masterVolume;

        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        AudioClip soundClip = null;
        if (currentSceneIdx%5!=0) //This needs to be checked once we have final scene
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
