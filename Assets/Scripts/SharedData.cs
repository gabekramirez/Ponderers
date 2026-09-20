using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SharedData : MonoBehaviour
{
    // AUDIO
    public enum GameAudioType
    {
        Music = 0,
        Player,
        SoundEffects,
        NumberOfGameAudioTypes
    };
    [HideInInspector] public static List<float> volumes;  // GameAudioType -> float
    public static GameAudioType gameAudioType;


    void Awake()
    {
        SharedData.volumes = new List<float>();
        // VOLUME
        for (int i = 0; i < (int)GameAudioType.NumberOfGameAudioTypes; i++)
        {
            SharedData.volumes.Add(1.0f);            
        }

    }

    public void ChangeVolume(){
        //GameAudioType.
    }
}
