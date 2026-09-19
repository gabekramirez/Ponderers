using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Manager : MonoBehaviour
{
    public Animator vignette_animator;
    private int current_level;
    void Start()
    {
        LoadNextLevel();
    }
    void LoadNextLevel()
    {
        //vignette_animator.Play("CloseLevel");
        vignette_animator.SetTrigger("SwapAnimation");
    }
}
