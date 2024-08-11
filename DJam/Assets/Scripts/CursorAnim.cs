using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CursorAnim : MonoBehaviour
{
    public static CursorAnim instance = null;

    int i = 0;
    int j = 0;
    public Texture2D[] idle;
    public Texture2D[] click;
    public bool isClicking;

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

    private void Start()
    {
        StartCoroutine(Running());
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            isClicking = true;
    }

    IEnumerator Running()
    {
        while (true)
        {
            if (isClicking)
            {
                while (j < click.Length)
                {
                    Cursor.SetCursor(click[j], Vector2.zero, CursorMode.Auto);
                    j++;
                    yield return new WaitForSeconds(0.05f);
                }
                i = 0;
                j = 0;
                isClicking = false;
            }
            Cursor.SetCursor(idle[i], Vector2.zero, CursorMode.Auto);
            i++;
            if (i == idle.Length)
                i = 0;
            yield return new WaitForSeconds(0.05f);
        }
    }
}
