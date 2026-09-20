using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class MenuController : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private GameObject vignetteAnimator;

    [Header("Audio Sliders")]
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider voicelineVolumeSlider;
    private AudioController audioController;

    void Start(){
        audioController = GameObject.Find("AudioController").transform.GetComponent<AudioController>();
        ExecuteAfterTime(0.1f, () => MusicManager.Instance.PlayMusic("MainMenu"));
    }

    public void PlayGame(){
        MusicManager.Instance.StopMusic();

        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIdx = currentSceneIdx+1;

        GameObject.Find("Canvas").SetActive(false);
        vignetteAnimator.SetActive(true);
        vignetteAnimator.transform.GetComponent<Animator>().SetTrigger("SwapAnimation");
        ExecuteAfterTime(1.5f, () => MoveScene(nextSceneIdx));
    }

    public void SetMasterVolume(){
        SharedData.masterVolume = masterVolumeSlider.value;
        audioController.Play_TestSound(SharedData.masterVolume);
    }

    public void SetMusicVolume(){
        //Set the volume
        SharedData.volumes[0] = musicVolumeSlider.value;
        float musicVol =  SharedData.volumes[(int)SharedData.GameAudioType.Music] * SharedData.masterVolume;
        audioController.Play_TestSound(musicVol);
    }   

    public void SetSFXVolume(){
        //Set the volume
        SharedData.volumes[1] = sfxVolumeSlider.value;
        float musicVol =  SharedData.volumes[(int)SharedData.GameAudioType.SoundEffects] * SharedData.masterVolume;
        audioController.Play_TestSound(musicVol);
    }

    public void SetVoicelineVolume(){
        SharedData.volumes[2] = sfxVolumeSlider.value;
        float musicVol =  SharedData.volumes[(int)SharedData.GameAudioType.Voicelines] * SharedData.masterVolume;
        audioController.Play_TestSound(musicVol);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void MoveScene(int sceneIdx){
        SceneManager.LoadScene(sceneIdx);
    }

    public void ExecuteAfterTime(float time, Action action)
    {
        StartCoroutine(ExecuteAfterTimeCoroutine(time, action));
    }

    private IEnumerator ExecuteAfterTimeCoroutine(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action?.Invoke();
    }

}
