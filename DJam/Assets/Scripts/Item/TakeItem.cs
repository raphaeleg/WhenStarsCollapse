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

    public void OnPointerDown()
    {
        OnButtonPressed();
    }

    public void OnButtonPressed()
    {
        // sfx.Play();
        if (gameObject.name == "MeteorA")
            GameObject.Find("A_List").GetComponent<CureStat>().totalMeteors++;
        if (gameObject.name == "MeteorB")
            GameObject.Find("B_List").GetComponent<CureStat>().totalMeteors++;
        if (gameObject.name == "MeteorC")
            GameObject.Find("C_List").GetComponent<CureStat>().totalMeteors++;
        Destroy(gameObject);
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

        // Direction
        gameObject.GetComponent<RectTransform>().localRotation = Quaternion.EulerRotation(0, 0, Mathf.Atan2(yMove, xMove));
    }

    private void Update()
    {
        destroyTime += Time.deltaTime;
        gameObject.GetComponent<MouseParallax>().origin += new Vector2(xMove * Time.deltaTime * 1000, yMove * Time.deltaTime * 1000);
        if (destroyTime > 5f)
            Destroy(gameObject);
    }
}
