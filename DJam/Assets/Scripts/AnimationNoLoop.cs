using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationNoLoop : MonoBehaviour
{
    public Sprite[] frames;
    public bool destroySelf = false;

    void Start()
    {
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        for (int i = 0; i < frames.Length; i++)
        {
            gameObject.GetComponent<UnityEngine.UI.Image>().sprite = frames[i];
            yield return new WaitForSeconds(0.1f);
        }
        if (destroySelf)
        {
            Destroy(gameObject);
        }
    }
}
