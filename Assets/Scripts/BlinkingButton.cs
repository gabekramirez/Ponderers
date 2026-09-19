using UnityEngine;
using UnityEngine.UI;

public class BlinkingButton : MonoBehaviour
{
    [Header("Blink Settings")]
    [SerializeField] private float blinkSpeed = 2f;
    [SerializeField] private float minimumVisibility = 0.2f;
    [SerializeField] private float maximumVisibility = 1f;

    private Image[] images;

    private void Awake()
    {
        // Find all Images on this object and its children
        images = GetComponentsInChildren<Image>(true);
    }

    private void Update()
    {
        // Smoothly oscillate between minimum and maximum visibility
        float visibility = Mathf.Lerp(
            minimumVisibility,
            maximumVisibility,
            (Mathf.Sin(Time.time * blinkSpeed) + 1f) / 2f
        );

        foreach (Image image in images)
        {
            Color color = image.color;
            color.a = visibility;
            image.color = color;
        }
    }
}