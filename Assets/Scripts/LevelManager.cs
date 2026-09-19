using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem; 

public class LevelManager : MonoBehaviour
{
    [Header("GameRequirements")]
    public float planningTimeRemaining; //Hidden from player?
    public float inputTimeRemaining=10f; //The time to put inputs in
    public bool timeRanOut = false;
    public bool firstInputPut = false;
    public bool victoryComplete = false;

    public string nextSceneName;
    public static int SCENE_COUNT = 2;


    [Header("UI Elements")]
    [SerializeField] private TMP_Text timeRemainingTXT;
    [SerializeField] private GameObject LevelEndPanel;
    [SerializeField] private GameObject TimeOutPanel;

    void Start(){
        //Initialization
        inputTimeRemaining = 10f;
        LevelManager.SCENE_COUNT = 2;
        timeRanOut = false;
        firstInputPut = false;
        victoryComplete = false;

        LevelEndPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Detect first input
        if (Keyboard.current != null && Keyboard.current.anyKey.wasPressedThisFrame)
            firstInputPut = true;

        timeRemainingTXT.text = $"Time Remaining: {inputTimeRemaining.ToString("N0")}";

        if(timeRanOut == false && firstInputPut == true && victoryComplete == false)
            inputTimeRemaining -= Time.deltaTime;

        if(inputTimeRemaining <= 0f){
            timeRanOut = true;
            TimeOutPanel.SetActive(true);
        }
    }

    public void AchieveVictory(){
        victoryComplete = true;
        LevelEndPanel.SetActive(true);
    }

    public void NextLevel(){
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIdx = currentSceneIdx+1;

        if(nextSceneIdx > LevelManager.SCENE_COUNT)
        {
            //Trigger final animation
        }else{
            SceneManager.LoadScene(nextSceneIdx);
        }
    }

    public void ReplayLevel(){
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIdx);
    }


}
