using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.UI;
using UnityEngine.UI;

public class TakeItem : MonoBehaviour
{
    public int posStart;
    private float xMove, yMove;
    private float destroyTime;

    public void OnButtonPressed()
    {
        // sfx.Play();
        transform.SetParent(GameObject.Find("MeteorsList").transform);
        gameObject.GetComponent<Button>().enabled = false;
        gameObject.GetComponent<ItemDrag>().enabled = true;
        this.enabled = false;
    }

    // Move
    private void Start()
    {
        xMove = Random.Range(-1f, 1f);
        yMove = Random.Range(-0.5f, 0.5f);
        if (xMove > -0.5f && xMove <= 0)
            xMove = -0.5f;
        else if (xMove > 0 && xMove < 0.5f)
            xMove = 0.5f;
        if (yMove > -0.25f && yMove <= 0)
            yMove = -0.25f;
        else if (yMove > 0 && yMove < 0.25f)
            yMove = 0.25f;

        if (posStart == 1 && xMove > 0)
            xMove = -xMove;
        else if (posStart == 2 && xMove < 0)
            xMove = -xMove;
        else if (posStart == 3 && yMove > 0)
            yMove = -yMove;
        else if (posStart == 4 && yMove < 0)
            yMove = -yMove;
    }

    private void Update()
    {
        destroyTime += Time.deltaTime;
        gameObject.GetComponent<RectTransform>().localPosition += new Vector3(xMove * Time.deltaTime * 1000, yMove * Time.deltaTime * 1000, 0);
        if (destroyTime > 5f)
            Destroy(gameObject);
    }
}
