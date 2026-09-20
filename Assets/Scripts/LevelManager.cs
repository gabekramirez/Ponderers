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
    float timeSpentPondering = 0f;

    public string nextSceneName;
    public static int SCENE_COUNT = 11;
    public static List<int> attemptsPerLevel;

    public AudioController audioController;

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

    //Called by Player_Main
    public void CalledStart(){
        //Initialization
        inputTimeRemaining = 10f;
        LevelManager.SCENE_COUNT = 11;
        timeRanOut = false;
        firstInputPut = false;
        victoryComplete = false;
        vignetteAnimator.gameObject.SetActive(true);
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
        SkipOffer.SetActive(true);

        endPieceRenderer.sprite = ResourceAssets.GetLevelPiece(currentSceneIdx);

        //Add button onClick sounds
        //Inactive is killing us
        GameObject canvas = GameObject.Find("Canvas");
        Button button1 = canvas.transform.Find("LevelEnd/Buttons/NextBTN").GetComponent<Button>();
        button1.onClick.AddListener(() => audioController.Add_ClickSound());


        canvas.transform.Find("LevelEnd/Buttons/ReplayBTN").transform.GetComponent<Button>().onClick.AddListener(() => audioController.Add_ClickSound());
        canvas.transform.Find("TimeRunOut/ReplayBTN").transform.GetComponent<Button>().onClick.AddListener(() => audioController.Add_ClickSound());
    }

    // Update is called once per frame
    void Update()
    {
        if(!victoryComplete && firstInputPut == false)
            timeSpentPondering += Time.deltaTime;
        if(timeSpentPondering >= 30f){
            //Play a voice line to let them know about skip button
        }

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

        if(Input.GetKeyDown(KeyCode.R))
            ReplayLevel();
    }

    public void AchieveVictory(){
        victoryComplete = true;
        LevelEndPanel.SetActive(true);

        audioController.Play_CollectSound();


        player.GetComponent<Player_Main>().enabled = false;
    }

    public void NextLevel(){

        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIdx = currentSceneIdx+1;

       /* if(nextSceneIdx > LevelManager.SCENE_COUNT)
        {
        }else{*/
            GameObject.Find("Canvas").SetActive(false);
            vignetteAnimator.SetActive(true);
            vignetteAnimator.transform.GetComponent<Animator>().SetTrigger("SwapAnimation");
            ExecuteAfterTime(1.5f, () => MoveScene(nextSceneIdx));
        //}
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
