using UnityEngine;
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class NewMonoBehaviourScript : MonoBehaviour
{
    public Sprite still_frame;
    public Sprite movement_1;
    public Sprite movment_2;

    private SpriteRenderer s_render;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        s_render = GetComponent<SpriteRenderer>();
        s_render.sprite = still_frame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
