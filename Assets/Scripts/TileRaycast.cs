using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastCheck : MonoBehaviour
{
    public float rayDistance = 10f;

    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = (worldPosition - (Vector2)transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            direction,
            rayDistance
        );

        Debug.DrawRay(
            transform.position,
            direction * rayDistance,
            Color.red
        );

        if (hit.collider != null)
        {
            Debug.Log("Collision with: " + hit.collider.gameObject.name);
        }
    }
}