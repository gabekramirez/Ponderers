using UnityEngine;

public class EndGate : MonoBehaviour
{
<<<<<<< Updated upstream
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
=======
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
>>>>>>> Stashed changes
