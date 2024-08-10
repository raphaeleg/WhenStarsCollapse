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
        Color originalColor = gameObject.GetComponent<Image>().color;
        gameObject.GetComponent<Image>().color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.25f);
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<Image>().color = new Color(originalColor.r, originalColor.g, originalColor.b, 1f);
        gameObject.GetComponent<ItemDrag>().enabled = true;
    }
}
