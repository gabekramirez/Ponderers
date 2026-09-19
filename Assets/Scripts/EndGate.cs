using UnityEngine;

public class EndGate : MonoBehaviour
{
    [Header("Script Reference")]
    private LevelManager levelManager;

    void Awake()
    {
        levelManager = GameObject.Find("Managers/LevelManager")
            .GetComponent<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Go to next level");
            levelManager.NextLevel();
        }
    }
}