using UnityEngine;
using System;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private CutsceneCharacter camera;
    [SerializeField] private SpriteRenderer scene;
    [SerializeField] private CutsceneCharacter louie;
    [SerializeField] private CutsceneCharacter beaver;
    [SerializeField] private CutsceneCharacter louieHouse;
    [SerializeField] private CutsceneCharacter beaverHouse;

    void Start()
    {
        scene.enabled = false;
        ExecuteAfterTime(1.0f, () => louie.GoTo(new Vector2(0, -8.75f), 1.0f));
        ExecuteAfterTime(1.0f, () => camera.GoTo(new Vector2(0, -6), 1.0f));
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
