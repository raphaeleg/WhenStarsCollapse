using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CureProcess : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        yield return new WaitForSeconds(5);
        gameObject.GetComponent<CureStat>().totalCures++;
        gameObject.GetComponent<CureStat>().totalMeteors = 0;
        gameObject.GetComponent<CureStat>().enabled = true;
        this.enabled = false;
    }
}
