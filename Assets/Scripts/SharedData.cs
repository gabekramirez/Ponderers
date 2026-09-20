using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SharedData : MonoBehaviour
{
    // AUDIO
    public enum GameAudioType
    {
        Music = 0,
        SoundEffects,
        Voicelines,
    };
    [HideInInspector] public static List<float> volumes;  // GameAudioType -> float
    public static GameAudioType gameAudioType;
    public static float masterVolume;

    void Awake()
    {
        SharedData.volumes = new List<float>();
        SharedData.masterVolume = 1f;

        // VOLUME
        for (int i = 0; i < Enum.GetNames(typeof(GameAudioType)).Length; i++)
        {
            SharedData.volumes.Add(1.0f);
        }
    }

    public void ChangeVolume(int volumeIdx, float volume){
        //GameAudioType.
        SharedData.volumes[volumeIdx] = volume;
    }
}
