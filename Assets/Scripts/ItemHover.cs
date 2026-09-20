using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class ItemHover : MonoBehaviour
{
    public enum Pickup_Type
    {
        DOOR,
        WALLS,
        WINDOW,
        ROOF
    }
    public Pickup_Type itemType = Pickup_Type.DOOR;
    private float elapsed;
    private float initial_y = 0f;
    const float MAX_OFFSET = 0.15f;
    private List<Sprite> sprite_list = new List<Sprite>();
    private SpriteRenderer s_renderer;
    void Start()
    {
        s_renderer = GetComponent<SpriteRenderer>();
        initial_y = transform.position.y;
        sprite_list.Add(Resources.Load<Sprite>("WoodDoor"));
        sprite_list.Add(Resources.Load<Sprite>("WoodWall"));
        sprite_list.Add(Resources.Load<Sprite>("WoodWindow"));
        sprite_list.Add(Resources.Load<Sprite>("WoodRoof"));
        s_renderer.sprite = sprite_list[(int)itemType];

    }
    // Update is called once per frame
    void Update()
    {
        if (elapsed > 255f)
        {
            elapsed = 0f;
        }
        elapsed += Time.deltaTime;

        float frame_height = 1 - 2 * Mathf.Abs(1 - (elapsed - Mathf.Floor(elapsed)) * 2);
        frame_height = Mathf.Sin(frame_height * Mathf.PI/2) * MAX_OFFSET;

        transform.position = new Vector3(transform.position.x, initial_y + frame_height, 0f);
    }
}
