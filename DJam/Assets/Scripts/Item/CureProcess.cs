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
        for (int i = 0; i < 500; i++)
        {
            transform.Find("ProcessBar").localScale = new Vector3((i+1)/500f, 1f, 1f);
            yield return new WaitForSeconds(0.01f);
        }
        gameObject.GetComponent<CureStat>().totalCures++;
        gameObject.GetComponent<CureStat>().totalMeteors = 0;
        gameObject.GetComponent<CureStat>().enabled = true;
        transform.Find("ProcessBar").localScale = new Vector3(0f, 1f, 1f);
        this.enabled = false;
    }
}
