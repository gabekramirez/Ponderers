using System;
using UnityEngine;

public class MovingSpike_Handler : MonoBehaviour
{
    public Vector3 position_offset = Vector3.zero;
    public float rotations_per_second = 0.5f;
    public float move_time = 1.0f;
    public float move_pause = 0.0f;
    private Vector3 initial_position;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initial_position = transform.position;
    }
    private float elapsed = 0.0f;
    private int direction_mult = 1;
    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        transform.localEulerAngles += Vector3.forward * 360 * rotations_per_second * Time.deltaTime;
        float progress = elapsed/move_time;
        if (elapsed > move_time + move_pause)
        {
            elapsed = 0;
            direction_mult *= -1;
        }else if (progress >= 1.0f)
        {
            progress = 1;
        }
        progress = (1 + Mathf.Sin(Mathf.PI * direction_mult * (0.5f - progress)))/2;
        transform.position = progress * position_offset + initial_position;
    }
}
