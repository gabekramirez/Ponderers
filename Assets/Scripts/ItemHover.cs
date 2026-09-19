using UnityEngine;

public class ItemHover : MonoBehaviour
{
    private float elapsed;
    private float initial_y = 0f;
    const float MAX_OFFSET = 0.15f;
    void Start()
    {
        initial_y = transform.position.y;
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
        print(frame_height);
        frame_height = Mathf.Sin(frame_height * Mathf.PI/2) * MAX_OFFSET;

        transform.position = new Vector3(transform.position.x, initial_y + frame_height, 0f);
    }
}
