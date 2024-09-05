using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int Seconds { get; private set; } = 0;

    public void Restart()
    { 
        Seconds = 0; 
    }

    private void Start()
    {
        Restart();
        StartCoroutine(UpdateTimer());
    }
    private IEnumerator UpdateTimer()
    {
        while (this.enabled == true)
        {
            yield return new WaitForSeconds(1);
            Seconds++;
            EventManager.TriggerEvent("TimerText", Seconds);
        }
    }
}
