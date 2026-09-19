using UnityEngine;

public class CollectibleEffect : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Floating")]
    [SerializeField] private float floatAmount = 0.08f;
    [SerializeField] private float floatSpeed = 2f;

    [Header("Pulse")]
    [SerializeField] private float pulseAmount = 0.08f;
    [SerializeField] private float pulseSpeed = 2.5f;

    [Header("Turning")]
    [SerializeField] private float turnAmount = 12f;
    [SerializeField] private float turnSpeed = 1.5f;

    [Header("Shimmer")]
    [SerializeField] private bool shimmer = true;
    [SerializeField] private float shimmerSpeed = 2f;
    [SerializeField] private float shimmerStrength = 0.35f;

    [Header("Glow")]
    [SerializeField] private bool glow = true;
    [SerializeField] private float glowAmount = 0.15f;
    [SerializeField] private float glowSpeed = 2f;

    private Vector3 startingPosition;
    private Vector3 startingScale;
    private Color startingColor;

    private float randomOffset;

    void Awake()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        startingPosition = transform.localPosition;
        startingScale = transform.localScale;
        startingColor = spriteRenderer.color;

        randomOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        float time = Time.time + randomOffset;

        AnimateFloat(time);
        AnimateScale(time);
        AnimateTurn(time);
        AnimateGlow(time);

        if (shimmer)
            AnimateShimmer(time);
    }

    private void AnimateFloat(float time)
    {
        float offset = Mathf.Sin(time * floatSpeed) * floatAmount;

        transform.localPosition = startingPosition + new Vector3(0f, offset, 0f);
    }

    private void AnimateScale(float time)
    {
        float pulse = Mathf.Sin(time * pulseSpeed) * pulseAmount;

        float scale = 1f + pulse;

        transform.localScale = startingScale * scale;
    }

    private void AnimateTurn(float time)
    {
        float rotation = Mathf.Sin(time * turnSpeed) * turnAmount;

        transform.localRotation = Quaternion.Euler(0f, 0f, rotation);
    }

    private void AnimateGlow(float time)
    {
        if (!glow)
            return;

        float glowPulse = (Mathf.Sin(time * glowSpeed) + 1f) / 2f;

        float brightness = 1f + glowPulse * glowAmount;

        Color color = startingColor * brightness;

        color.a = startingColor.a;

        spriteRenderer.color = color;
    }

    private void AnimateShimmer(float time)
    {
        float shimmerPosition = Mathf.Repeat(time * shimmerSpeed, 2f) - 1f;

        // Creates a bright band that moves across the sprite.
        float distance = Mathf.Abs(shimmerPosition);

        float shimmerValue = Mathf.Clamp01(1f - distance * 3f);

        Color color = spriteRenderer.color;

        color.r += shimmerValue * shimmerStrength;
        color.g += shimmerValue * shimmerStrength;
        color.b += shimmerValue * shimmerStrength;

        color.a = startingColor.a;

        spriteRenderer.color = color;
    }
}