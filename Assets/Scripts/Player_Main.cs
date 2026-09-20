using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player_Main : MonoBehaviour
{
    public Sprite still_frame;
    public Sprite[] movement_frames;
    public Player_Main player_copy;
    public LayerMask ray_mask = 3;

    const float SPEED = 15.0f;
    const float ACCEL = 30.0f;
    float current_speed = 0f;
    private Vector3 movement_vector = Vector3.zero;
    private SpriteRenderer s_render;
    private float sprite_anim_time = 0f;
    private int input_frames = 0;
    private int collision_buffer_frames = 0;
    private Vector3 buffer_direction = Vector3.zero;
    private List<Door_Handler> doors = new List<Door_Handler>();
    private LevelManager levelManager;
    private ContactFilter2D contact_filter = new ContactFilter2D();

    [Header("Audio")]
    private GameObject audioControllerPrefab;
    private GameObject audioPrefab;
    private Audio audio;
    private AudioClip walkClip;
    private AudioClip wallHitClip;
    private AudioController audioController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        contact_filter.SetLayerMask((LayerMask)0);
        s_render = GetComponent<SpriteRenderer>();
        s_render.sprite = still_frame;
        levelManager = GameObject.Find("Managers/LevelManager").GetComponent<LevelManager>();
        Object[] scene_items = FindObjectsByType<Door_Handler>();
        for (int i = 0; i < scene_items.Length; i++)
        {
            doors.Add(scene_items[i] as Door_Handler);
        }

        walkClip = Resources.Load<AudioClip>("SFX/walk");
        wallHitClip = Resources.Load<AudioClip>("SFX/thud");

        audioControllerPrefab = Resources.Load<GameObject>("AudioController");

        audioPrefab = Resources.Load<GameObject>("Audio");
        audio = GameObject.Instantiate(audioPrefab).transform.GetComponent<Audio>();

        audioController = Instantiate(audioControllerPrefab).transform.GetComponent<AudioController>();
        audioController.RanAwake();
        levelManager.audioController = audioController;
        levelManager.CalledStart();
        
    }

    void OnMove(InputValue value)
    {
        
        if (player_copy)
        {
            player_copy.OnMove(value);
        }
        Vector2 input_vec = value.Get<Vector2>();
        if (Mathf.Abs(input_vec.x) > 0f && Mathf.Abs(input_vec.y) > 0f)
        {
            input_vec = Vector2.right * Mathf.Sign(input_vec.x);
        }
        
        if (input_vec.magnitude > 0f && movement_vector.magnitude == 0f)
        {
            movement_vector = new Vector3(input_vec.x, input_vec.y, 0f);
        }
        else if (input_vec.magnitude > 0f)
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

        sprite_anim_time += Time.deltaTime * 8;
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position, movement_vector, 0.5f, ray_mask);
        
        if (rayHit && rayHit.collider.CompareTag("Untagged") && rayHit.collider != gameObject)
        {
            transform.position = new Vector3(rayHit.point.x, rayHit.point.y, 0f) - movement_vector * .5f;
            movement_vector = Vector3.zero;

            // Play wall hit sound
            audio.PlayForce(wallHitClip);

            if (input_frames < 10)
            {
                movement_vector = buffer_direction;
            }

            collision_buffer_frames = 0;
        }
        else if (rayHit && rayHit.collider.CompareTag("Finish"))
        {
            //trigger win here
            levelManager.AchieveVictory();
        }else if (rayHit && rayHit.collider.CompareTag("Levers"))
        {
            
            rayHit.collider.GetComponent<SpriteResolver>().SetCategoryAndLabel("Main", "Lever_1");
            for (int i = 0; i < doors.Count; i++)
            {
                doors[i].OnLever();
            }
        }else if (rayHit && rayHit.collider.CompareTag("Spikes") && levelManager.victoryComplete == false)
        {
            print("hit spike");
            levelManager.inputTimeRemaining = -1f;
            
            audioController.Play_DeathVoiceline();
        }

        if (movement_vector.magnitude > 0f)
        {
            s_render.sprite = movement_frames[
                Mathf.FloorToInt(sprite_anim_time) % movement_frames.Length
            ];

            // Play walking sound
            audio.Play(walkClip, 0.5f);
        }
        else if (collision_buffer_frames > 5)
        {
            s_render.sprite = still_frame;
            current_speed = 0f;

            // Reset walk sound timer
            audio.ResetWalkTimer();
        }



    }
    void FixedUpdate()
    {
        input_frames++;
        collision_buffer_frames++;
    }
}
