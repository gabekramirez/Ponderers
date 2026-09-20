using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private GameObject audioPrefab;

    public void Add_ClickSound(){
        GameObject newAudio = Instantiate(audioPrefab);
        Audio audioComp = newAudio.transform.GetComponent<Audio>();

        float volume = SharedData.volumes[(int)audioComp.audioType] * SharedData.masterVolume;
        audioComp.play();
    }
}
