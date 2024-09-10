using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int Seconds { get; private set; } = -1;
    private bool Pause = false;

    private void Start()
    {
        Restart();
        StartCount();
    }
    public void Restart() { Seconds = -1; }
    public void SetPause(bool paused) {  Pause = paused; }
    public void StartCount() { StartCoroutine(UpdateTimer()); }
    private IEnumerator UpdateTimer()
    {
        while (!Pause)
        {
            Seconds++;
            EventManager.TriggerEvent("TimerText", Seconds);
            yield return new WaitForSeconds(1);
        }
    }
}
