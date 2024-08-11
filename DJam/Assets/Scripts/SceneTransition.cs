using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{
    private static SceneTransition instance = null;
    private Image image;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if (this != instance)
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayTransition()
    {
        image = transform.Find("Image").GetComponent<Image>();
        StartCoroutine(Running());
    }

    IEnumerator Running()
    {
        transform.Find("Image").gameObject.SetActive(true);
        float i = 0f;

        while (i <= 0.5f)
        {
            image.color = new Color(0,0,0, i / 0.5f);
            i += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(0.6f);

        while (i <= 1f)
        {
            image.color = new Color(0,0,0, (1f - i) / 0.5f);
            i += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        transform.Find("Image").gameObject.SetActive(false);
    }
}
