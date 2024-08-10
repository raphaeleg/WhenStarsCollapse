using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    public Sprite[] frames;

    void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        while (true)
        {
            for (int i = 0; i < frames.Length; i++)
            {
                gameObject.GetComponent<UnityEngine.UI.Image>().sprite = frames[i];
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
