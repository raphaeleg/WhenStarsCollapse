using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Animation : MonoBehaviour
{
    public Sprite[] frames;
    public float rate = 0.1f;
    public bool loop = true;

    public void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        while (true)
        {
            for (int i = 0; i < frames.Length; i++)
            {
                gameObject.GetComponent<Image>().sprite = frames[i];
                yield return new WaitForSeconds(rate);
            }
            if (!loop)
                break;
        }
    }
}
