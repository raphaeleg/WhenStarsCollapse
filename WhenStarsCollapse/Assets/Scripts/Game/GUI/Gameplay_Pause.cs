using UnityEngine;

/// <summary>
/// Handles timeScale in Gameplay
/// </summary>
public class Gameplay_Pause : MonoBehaviour
{
    void OnEnable()
    {
        Time.timeScale = 0;
    }
    void OnDisable()
    {
        Time.timeScale = 1;
    }
}
