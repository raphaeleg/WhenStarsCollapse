using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public int score = 0; // The score here

    void Start()
    {
        StartCoroutine(PerSecond());
    }

    IEnumerator PerSecond()
    {
        yield return new WaitForSeconds(1);
        while (this.enabled == true)
        {
            score++;
            yield return new WaitForSeconds(1);
        }
    }
}
