using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CureProcess : MonoBehaviour
{
    void Update()
    {
        StartCoroutine(Running());
        this.enabled = false;
    }

    IEnumerator Running()
    {
        for (int i = 0; i < 100; i++)
        {
            transform.Find("ProcessBar").localScale = new Vector3((i+1)/100f, 1f, 1f);
            yield return new WaitForSeconds(0.01f);
        }
        gameObject.GetComponent<CureStat>().totalCures++;
        gameObject.GetComponent<CureStat>().totalMeteors = 0;
        gameObject.GetComponent<CureStat>().enabled = true;
        transform.Find("ProcessBar").localScale = new Vector3(0f, 1f, 1f);
    }
}
