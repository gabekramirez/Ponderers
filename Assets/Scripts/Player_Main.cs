using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class NewMonoBehaviourScript : MonoBehaviour
{
    public Sprite still_frame;
    public Sprite[] movement_frames;

    const float SPEED = 15.0f;
    const float ACCEL = 50.0f;
    float current_speed = 0f;
    private Vector3 movement_vector = Vector3.zero;
    private SpriteRenderer s_render;
    private float sprite_anim_time = 0f;
    private int input_frames = 0;
    private Vector3 buffer_direction = Vector3.zero;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        s_render = GetComponent<SpriteRenderer>();
        s_render.sprite = still_frame;
    }

    void OnMove(InputValue value)
    {
        Vector2 input_vec = value.Get<Vector2>();
        if (Mathf.Abs(input_vec.x) > 0f && Mathf.Abs(input_vec.y) > 0f)
        {
            input_vec = Vector2.right * Mathf.Sign(input_vec.x);
        }
        
        if (input_vec.magnitude > 0f && movement_vector.magnitude == 0f)
        {
            movement_vector = new Vector3(input_vec.x, input_vec.y, 0f);
        }else if (input_vec.magnitude > 0f)
        {
            input_frames = 0;
            buffer_direction = new Vector3(input_vec.x, input_vec.y, 0f);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        current_speed += ACCEL * Time.deltaTime;
        current_speed = Mathf.Clamp(current_speed, 0f, SPEED);
        transform.position += movement_vector * current_speed * Time.deltaTime;

        sprite_anim_time += Time.deltaTime;
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, movement_vector, 0.5f);
        if (rayHit && rayHit.collider.CompareTag("Untagged"))
        {
            transform.position = new Vector3(rayHit.point.x, rayHit.point.y, 0f) - movement_vector * .5f;
            movement_vector = Vector3.zero;
            if (input_frames < 20)
            {
                movement_vector = buffer_direction;
            }
        }
        if (movement_vector.magnitude > 0f)
        {
            s_render.sprite = movement_frames[Mathf.FloorToInt(sprite_anim_time) % movement_frames.Length];
        }
        else
        {
            s_render.sprite = still_frame;
            current_speed = 0f;
        }
    }
    void FixedUpdate()
    {
        input_frames++;
    }
}
