using UnityEngine;

public class TutorialButtons : MonoBehaviour
{
    public void Next()
    {
        EventManager.TriggerEvent("Tutorial_Next", 1);
    }
    public void Back()
    {
        EventManager.TriggerEvent("Tutorial_Next", -1);
    }
}
