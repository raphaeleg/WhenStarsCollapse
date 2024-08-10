using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public int timer = 0;
    public int stars = 0;
    public int blackHoles = 0;

    void Start()
    {
        StartCoroutine(PerSecond());
    }

    IEnumerator PerSecond()
    {
        yield return new WaitForSeconds(1);
        while (this.enabled == true)
        {
            timer++;
            yield return new WaitForSeconds(1);
        }
    }
}
