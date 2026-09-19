using UnityEngine;
using UnityEngine.U2D.Animation;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteResolver))]
public class Door_Handler : MonoBehaviour
{
    private Animator d_animator;
    private SpriteResolver d_resolve;
    private BoxCollider2D d_collider;
    public enum door_types
    {
        Left_Right,
        Up_Down
    }
    public door_types door_type = door_types.Left_Right;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        d_animator = GetComponent<Animator>();
        d_resolve = GetComponent<SpriteResolver>();
        d_collider = GetComponent<BoxCollider2D>();

        if (door_type == door_types.Left_Right)
        {
            d_resolve.SetCategoryAndLabel("LeftRight", "LeftRightGate_0");
        }
        else
        {
            d_animator.SetBool("Is_UD", true);
            d_resolve.SetCategoryAndLabel("UpDown", "UpDownGate_0");
        }
    }

    public void OnLever()
    {
        d_animator.SetTrigger("Open");
        d_collider.enabled = false;
        print("all doors opened!");
    }
}
