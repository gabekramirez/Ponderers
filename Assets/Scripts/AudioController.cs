using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    private Audio audio;
    [Header("Audio Clips")]
    private AudioClip clickSound;
    private AudioClip testSound;
    private AudioClip loseSound;

    [Header("Game Sounds")]
    private List<AudioClip> collectionSounds;
    private AudioClip bigWinSound;
    private GameObject audioPrefab;

    [Header("Voicelines")]
    private List<AudioClip> deathSoundClips;
    private List<AudioClip> skipPrompts;

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
        loseSound = Resources.Load<AudioClip>("SFX/lose");

        collectionSounds = new List<AudioClip>();
        // Make sure the capitalization matches your actual file names perfectly
        collectionSounds.Add(Resources.Load<AudioClip>("SFX/Collect1")); 
        collectionSounds.Add(Resources.Load<AudioClip>("SFX/collect2"));
        collectionSounds.Add(Resources.Load<AudioClip>("SFX/collect3"));

        //Add the death sounds
        deathSoundClips = new List<AudioClip>();
        deathSoundClips.Add(Resources.Load<AudioClip>("Archive/Death1"));
        deathSoundClips.Add(Resources.Load<AudioClip>("Archive/Death2"));
        deathSoundClips.Add(Resources.Load<AudioClip>("Archive/Death3"));
        deathSoundClips.Add(Resources.Load<AudioClip>("Archive/Death4"));
        deathSoundClips.Add(Resources.Load<AudioClip>("Archive/Death5"));
        deathSoundClips.Add(Resources.Load<AudioClip>("Archive/Death6"));

        //Add the skip sounds
        skipPrompts = new List<AudioClip>();
        skipPrompts.Add(Resources.Load<AudioClip>("Archive/Skip1"));
        skipPrompts.Add(Resources.Load<AudioClip>("Archive/Skip2"));
        skipPrompts.Add(Resources.Load<AudioClip>("Archive/Skip3"));
        skipPrompts.Add(Resources.Load<AudioClip>("Archive/Skip4"));

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

    public void Play_LoseSound(){
        float volume = SharedData.volumes[1] * SharedData.masterVolume;
        audio.Play(loseSound, volume);
    }

    public void Play_DeathVoiceline(){
        int randomChance = UnityEngine.Random.Range(0, 3);
        if(randomChance != 1)
            return;

        float volume = SharedData.volumes[2] * SharedData.masterVolume;
        AudioClip randomDeathSound = deathSoundClips[UnityEngine.Random.Range(0,deathSoundClips.Count-1)];
        audio.PlayForce(randomDeathSound, volume);
    }

    public void Play_SkipVoiceline(){
        int randomChance = UnityEngine.Random.Range(0, 3);
        if(randomChance != 1)
            return;

        float volume = SharedData.volumes[2] * SharedData.masterVolume;
        AudioClip randomSkipEffect = skipPrompts[UnityEngine.Random.Range(0,skipPrompts.Count-1)];
        audio.PlayForce(randomSkipEffect, volume);
      
    }
}
