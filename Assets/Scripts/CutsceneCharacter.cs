using System.Collections.Generic;
using UnityEngine;

public class CutsceneCharacter : MonoBehaviour
{
    [SerializeField] private List<Sprite> walkAnimation;
    private Vector2 targetPosition;
    private float time = 0.0f;
    private float animationTime = 1.0f;

    public void GoTo(Vector2 goPosition, float goTime)
    {
        targetPosition = goPosition;
        time = 0.0f;
        animationTime = goTime;
    }

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= animationTime)
        {
            time = animationTime;
        }
        transform.position = Vector2.Lerp(transform.position, targetPosition, time / animationTime);
    }
}
