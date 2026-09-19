using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CutsceneCharacter : MonoBehaviour
{
    [SerializeField] private List<Sprite> walkAnimation;
    private List<Vector2> path;

    void Update()
    {
        // Transform.position.x = 0;
    }
}
