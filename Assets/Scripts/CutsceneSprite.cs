using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CutsceneSprite : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites;
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private float time = 1.0f;
    private float animationTime = 1.0f;
    private float startScale;
    private SpriteRenderer spriteRenderer;

    public void ChangeSprite(int index)
    {
        spriteRenderer.sprite = sprites[index];
    }

    public void SetSize(float scale)
    {
        spriteRenderer.flipX = true;
    }

    public void Flip()
    {
        spriteRenderer.flipX = true;
    }

    public void Unflip()
    {
        spriteRenderer.flipX = false;
    }

    public void Show()
    {
        spriteRenderer.enabled = true;
    }

    public void Hide()
    {
        spriteRenderer.enabled = false;
    }

    public void GoTo(Vector2 goPosition, float goTime)
    {
        startPosition = transform.position;
        targetPosition = goPosition;
        time = 0.0f;
        animationTime = goTime;
        if (animationTime == 0.0f)
        {
            transform.position = goPosition;
        }
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetPosition = transform.position;
    }

    void Update()
    {
        if (animationTime != 0.0f)
        {
            time += Time.deltaTime;
            if (sprites.Count > 0)
            {
                int frame = (int)(time * 10) % sprites.Count;
                if (time >= animationTime)
                {
                    time = animationTime;
                    frame = 0;
                }
                ChangeSprite(frame);
            }
            transform.position = Vector2.Lerp(startPosition, targetPosition, time / animationTime);
        }
    }
}
