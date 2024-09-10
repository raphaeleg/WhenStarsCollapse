using UnityEngine;

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
