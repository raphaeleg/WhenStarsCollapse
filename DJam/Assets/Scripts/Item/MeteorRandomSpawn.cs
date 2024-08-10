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
        yield return new WaitForSeconds(1);
        while (this.enabled == true)
        {
            GameObject meteor = Instantiate(meteors[Random.Range(0, meteors.Length)]);
            meteor.transform.SetParent(parentTarget);
            meteor.name = meteor.name.Substring(0, meteor.name.Length - 7);

            float maxWidth = canvas.rect.width + canvas.rect.x;
            float maxHeight = canvas.rect.height + canvas.rect.y;

            int isHorizontalPos = Random.Range(0,2);
            if (isHorizontalPos == 0)
            {
                // VerticalPos
                int isLeft = Random.Range(0, 2);
                if (isLeft == 0)
                {
                    // isRight
                    meteor.GetComponent<TakeItem>().posStart = 1;
                    meteor.transform.localPosition = new Vector2(maxWidth + 50, Random.Range(-maxHeight + 50, maxHeight - 50)/2);
                }
                else
                {
                    // isLeft
                    meteor.GetComponent<TakeItem>().posStart = 2;
                    meteor.transform.localPosition = new Vector2(-maxWidth - 50, Random.Range(-maxHeight + 50, maxHeight - 50)/2);
                }
            }
            else
            {
                // HorizontalPos
                int isTop = Random.Range(0, 2);
                if (isTop == 0)
                {
                    // isTop
                    meteor.GetComponent<TakeItem>().posStart = 3;
                    meteor.transform.localPosition = new Vector2(Random.Range(-maxWidth + 50, maxWidth - 50)/2, maxHeight + 50);
                }
                else
                {
                    // isBottom
                    meteor.GetComponent<TakeItem>().posStart = 4;
                    meteor.transform.localPosition = new Vector2(Random.Range(-maxWidth + 50, maxWidth - 50)/2, -maxHeight - 50);
                }
            }
                
            yield return new WaitForSeconds(1);
        }
    }
}
