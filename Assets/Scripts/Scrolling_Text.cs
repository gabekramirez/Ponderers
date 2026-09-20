using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]

[Serializable] public class CutsceneText
{
    public String text_segment = "";
    public float end_delay = 1.0f;
    public float time_per_letter = 0.01f;
}

public class Scrolling_Text : MonoBehaviour
{
    public CutsceneText[] texts;
    
    private Text t_text;
    private int current_text_int = 0;
    float elapsed = 0f;

    void Start()
    {
        t_text = GetComponent<Text>();
    }

    void Update()
    {
        if (current_text_int >= texts.Length)
        {
            return;
        }

        elapsed += Time.deltaTime;
        CutsceneText current_text = texts[current_text_int];
        float max_time = current_text.time_per_letter * current_text.text_segment.Length;

        float progress = Mathf.Clamp(elapsed/max_time, 0f, 1f);
        String current_string = current_text.text_segment.Substring(0, (int)(progress * current_text.text_segment.Length));
        t_text.text = current_string;
        if (elapsed >= (max_time + current_text.end_delay))
        {
            elapsed = 0f;
            current_text_int++;
        }
    }
}
