using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem; 
using System;
using System.Collections;

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
    public static List<int> attemptsPerLevel;

    [Header("UI Elements")]
    [SerializeField] private TMP_Text timeRemainingTXT;
    [SerializeField] private GameObject LevelEndPanel;
    [SerializeField] private GameObject TimeOutPanel;
    [SerializeField] private GameObject SkipOffer;

    [Header("Animation")]
    [SerializeField] private GameObject vignetteAnimator;

    [Header("Gameplay Objects")]
    [SerializeField] private Transform louieStart;
    [SerializeField] private Transform player;
    [SerializeField] private SpriteRenderer endPieceRenderer;

    void Start(){
        //Initialization
        inputTimeRemaining = 10f;
        LevelManager.SCENE_COUNT = 2;
        timeRanOut = false;
        firstInputPut = false;
        victoryComplete = false;

        if(LevelManager.attemptsPerLevel == null){
            LevelManager.attemptsPerLevel = new List<int>();
            for(int i = 0; i < 25; i++){
                LevelManager.attemptsPerLevel.Add(0);
            }
        }

        LevelEndPanel.SetActive(false);

        //Set LOUIE THE LUMBERJACK to his start position
        player.position = louieStart.position;
        
        //Offer skip 
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        int attempts = LevelManager.attemptsPerLevel[currentSceneIdx];
        SkipOffer.SetActive(attempts>0);

        endPieceRenderer.sprite = ResourceAssets.GetLevelPiece(currentSceneIdx);
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
            player.GetComponent<Player_Main>().enabled = false;
        }
    }

    public void AchieveVictory(){
        victoryComplete = true;
        LevelEndPanel.SetActive(true);

        player.GetComponent<Player_Main>().enabled = false;
    }

    public void NextLevel(){

        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIdx = currentSceneIdx+1;

        if(nextSceneIdx > LevelManager.SCENE_COUNT)
        {
        }else{
            vignetteAnimator.SetActive(true);
            vignetteAnimator.transform.GetComponent<Animator>().SetTrigger("SwapAnimation");
            ExecuteAfterTime(5.0f, () => MoveScene(nextSceneIdx));
        }
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

    public void ReplayLevel(){
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;

        //Trigger final animation
        LevelManager.attemptsPerLevel[currentSceneIdx]++;

        SceneManager.LoadScene(currentSceneIdx);
    }


}
