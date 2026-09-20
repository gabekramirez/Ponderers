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

    public void PlayGame(){
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIdx = currentSceneIdx+1;

        GameObject.Find("Canvas").SetActive(false);
        vignetteAnimator.SetActive(true);
        vignetteAnimator.transform.GetComponent<Animator>().SetTrigger("SwapAnimation");
        ExecuteAfterTime(1.5f, () => MoveScene(nextSceneIdx));
    }

    public void SetMasterVolume(){
        SharedData.masterVolume = masterVolumeSlider.value;
    }

    public void SetMusicVolume(){

    }

    public void SetSFXVolume(){

    }

    public void SetVoicelineVolume(){

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
