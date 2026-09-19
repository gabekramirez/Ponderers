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
    [HideInInspector] public List<float> volumes;  // GameAudioType -> float





    // BOILER PLATE
    private static SharedData _instance;
    public static SharedData Instance {get {return _instance;}}

    void Awake()
    {
        // VOLUME
        for (int i = 0; i < (int)GameAudioType.NumberOfGameAudioTypes; i++)
        {
            volumes.Add(1.0f);            
        }

        // BOILER PLATE
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }
}