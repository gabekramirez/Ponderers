using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(Audio))]
public class TestAudio : MonoBehaviour
{
    Audio audio;
    public void Awake()
    {
        audio = GetComponent<Audio>();
    }

    public void Start()
    {
        if (audio.isParent)
        {
            ExecuteAfterTime(0.1f, () => audio.play());
            ExecuteAfterTime(0.2f, () => audio.play());
            ExecuteAfterTime(0.3f, () => audio.play());
        }
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