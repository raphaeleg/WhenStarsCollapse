using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRandomSpawn : MonoBehaviour
{
    public GameObject[] meteors;
    public Transform parentTarget;
    public RectTransform canvas;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(3);
        while (this.enabled == true)
        {
            GameObject meteor = Instantiate(meteors[Random.Range(0, meteors.Length)]);
            meteor.transform.SetParent(parentTarget);
            meteor.name = meteor.name.Substring(0, meteor.name.Length - 7);

            float maxWidth = canvas.rect.width + canvas.rect.x;
            float maxHeight = canvas.rect.height + canvas.rect.y;

            meteor.transform.localPosition = new Vector2(Random.Range(-maxWidth + 50, maxWidth - 50), Random.Range(-maxHeight + 50, maxHeight - 50));

            yield return new WaitForSeconds(3);
        }
    }
}
