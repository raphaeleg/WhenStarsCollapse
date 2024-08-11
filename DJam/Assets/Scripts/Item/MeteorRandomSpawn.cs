using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRandomSpawn : MonoBehaviour
{
    public GameObject[] meteors;

    public Transform parentTarget;
    public RectTransform canvas;
    private float maxWidth = 0;
    private float maxHeight = 0;
    private const float padding = 50;

    private void Start()
    {
        StartCoroutine(Spawner());
        maxWidth = canvas.rect.width + canvas.rect.x;
        maxHeight = canvas.rect.height + canvas.rect.y;
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(1);
        while (this.enabled == true)
        {
            GameObject meteor = Instantiate(meteors[Random.Range(0, meteors.Length)]);
            meteor.transform.SetParent(parentTarget);
            meteor.name = meteor.name.Substring(0, meteor.name.Length - 7); // Remove "(Clone)"

            int isHorizontalPos = Random.Range(0,2);
            if (isHorizontalPos == 0)
            {
                var x = maxWidth + padding;
                var y = Random.Range(-maxHeight + padding, maxHeight - padding) / 2;

                // VerticalPos
                int isLeft = Random.Range(0, 2);
                if (isLeft == 0)
                {
                    // isRight
                    meteor.GetComponent<TakeItem>().posStart = 1;
                    meteor.transform.localPosition = new Vector2(x, y);
                }
                else
                {
                    // isLeft
                    meteor.GetComponent<TakeItem>().posStart = 2;
                    meteor.transform.localPosition = new Vector2(-x, y);
                }
            }
            else
            {
                var x = Random.Range(-maxWidth + padding, maxWidth - padding) / 2;
                var y = maxHeight + padding;
                // HorizontalPos
                int isTop = Random.Range(0, 2);
                if (isTop == 0)
                {
                    // isTop
                    meteor.GetComponent<TakeItem>().posStart = 3;
                    meteor.transform.localPosition = new Vector2(x, y);
                }
                else
                {
                    // isBottom
                    meteor.GetComponent<TakeItem>().posStart = 4;
                    meteor.transform.localPosition = new Vector2(x, -y);
                }
            }
                
            yield return new WaitForSeconds(0.85f);
        }
    }
}
