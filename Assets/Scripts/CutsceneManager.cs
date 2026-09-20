using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private int cutsceneToPlay;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private CutsceneSprite rightCover;
    [SerializeField] private CutsceneSprite camera;
    [SerializeField] private CutsceneSprite scene;
    [SerializeField] private CutsceneSprite louie;
    [SerializeField] private CutsceneSprite beaver;
    [SerializeField] private CutsceneSprite louieHouse;
    [SerializeField] private GameObject vignetteAnimator;
    [SerializeField] private List<Audio> audio;

    void Start()
    {
        int currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIdx = currentSceneIdx+1;

        switch(cutsceneToPlay)
        {
            case 0:
                // frame 1
                scene.enabled=true;
                scene.GoTo(new Vector2(3, 0f), 0.0f);
                scene.ChangeSprite(0);
                louie.Hide();
                PlayAudio(0);

                // frame 2
                ExecuteAfterTime(7f, () => {
                    PlayAudio(1);
                    scene.GoTo(new Vector2(0, 0f), 0.0f);
                    scene.ChangeSprite(1);
                });

                // top down
                ExecuteAfterTime(15.5f, () => {
                    PlayAudio(2);
                    scene.ChangeSprite(2);
                    louie.GoTo(new Vector2(-2.5f, 0), 0.0f);
                    louie.Show();
                });
                // run down
                ExecuteAfterTime(16.5f, () => louie.GoTo(new Vector2(-2.5f, -1.5f), 0.5f));
                // run right
                ExecuteAfterTime(17f, () => louie.GoTo(new Vector2(7.0f, -1.5f), 0.5f));

                // end
                ExecuteAfterTime(17.5f, () => {
                    vignetteAnimator.SetActive(true);
                    vignetteAnimator.transform.GetComponent<Animator>().SetTrigger("SwapAnimation");
                });
                ExecuteAfterTime(19.5f, () => {
                    SceneManager.LoadScene(nextSceneIdx);
                });
                break;
            case 1:
                // frame 1
                scene.enabled=true;
                scene.GoTo(new Vector2(0f, 0f), 0.0f);
                scene.ChangeSprite(0);
                louie.Hide();
                PlayAudio(0);

                // topdown
                ExecuteAfterTime(3f, () => {
                    PlayAudio(1);
                    scene.ChangeSprite(1);
                    camera.GoTo(new Vector2(-3f, 0), 0.0f);
                    rightCover.GoTo(new Vector2(5f, 0), 0.0f);
                    louie.GoTo(new Vector2(-1f, 0), 0.0f);
                    louie.Flip();
                    louieHouse.Show();
                    louie.Show();
                });

                // pan camera
                ExecuteAfterTime(4.5f, () => {
                    louie.Unflip();
                    camera.GoTo(new Vector2(0, 0), 1.5f);
                    rightCover.GoTo(new Vector2(10.75f, 0), 1.5f);
                });

                // frame 2
                ExecuteAfterTime(9f, () => {
                    louieHouse.Hide();
                    louie.Hide();
                    PlayAudio(2);
                    scene.ChangeSprite(2);
                });

                // end
                ExecuteAfterTime(13f, () => {
                    vignetteAnimator.SetActive(true);
                    vignetteAnimator.transform.GetComponent<Animator>().SetTrigger("SwapAnimation");
                });
                ExecuteAfterTime(15f, () => {
                    SceneManager.LoadScene(nextSceneIdx);
                });
                break;
            case 2:
                break;
        }
    }

    private void PlayAudio(int index)
    {
        audio[index].Play(audio[index].gameObject.GetComponent<AudioSource>().clip);
    }

    // Noah's execute after time thing
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
