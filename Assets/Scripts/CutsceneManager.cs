using UnityEngine;
using System;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private CutsceneCharacter camera;
    [SerializeField] private CutsceneCharacter scene;
    [SerializeField] private CutsceneCharacter louie;
    [SerializeField] private CutsceneCharacter beaver;
    [SerializeField] private CutsceneCharacter louieHouse;
    [SerializeField] private CutsceneCharacter beaverHouse;
    [SerializeField] private GameObject vignetteAnimator;

    void Start()
    {
        scene.enabled=true;
        scene.GoTo(new Vector2(3, 0f), 0.0f);
        scene.ChangeSprite(0);
        louie.Hide();
        // background.color = new Color(0, 0, 0);

        // next frame
        ExecuteAfterTime(1.0f, () => {
            scene.GoTo(new Vector2(0, 0f), 0.0f);
            scene.ChangeSprite(1);
        });

        // down
        ExecuteAfterTime(3.0f, () => {
            scene.ChangeSprite(2);
            louie.GoTo(new Vector2(-2.5f, 0), 0.0f);
            louie.Show();
        });
        ExecuteAfterTime(4.0f, () => {
            louie.GoTo(new Vector2(-2.5f, -1.5f), 2.0f);
        });

        // right
        ExecuteAfterTime(4.5f, () => louie.GoTo(new Vector2(5.3f, -1.5f), 2.0f));

        ExecuteAfterTime(4.5f, () => {
            vignetteAnimator.SetActive(true);
            vignetteAnimator.transform.GetComponent<Animator>().SetTrigger("SwapAnimation");
        });
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
