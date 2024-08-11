using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CureStat : MonoBehaviour
{
    public int totalMeteors = 0;
    public int totalCures = 0;
    private Transform resourceBar;
    private TMP_Text text;

    public Sprite inactive;
    public Sprite active;

    private void Start()
    {
        resourceBar = transform.Find("ResourceBar");
        text = transform.Find("Count").GetComponent<TMP_Text>();
    }

    void Update()
    {
        text.text = totalCures.ToString();
        if (totalCures > 0)
        {
            if (gameObject.name == "A_List")
            {
                GameObject.Find("CureA").GetComponent<ItemDrag>().enabled = true;
                GameObject.Find("CureA").GetComponent<Image>().sprite = active;
            }
            if (gameObject.name == "B_List")
            {
                GameObject.Find("CureB").GetComponent<ItemDrag>().enabled = true;
                GameObject.Find("CureB").GetComponent<Image>().sprite = active;
            }
            if (gameObject.name == "C_List")
            {
                GameObject.Find("CureC").GetComponent<ItemDrag>().enabled = true;
                GameObject.Find("CureC").GetComponent<Image>().sprite = active;
            }
        }
        else
        {
            if (gameObject.name == "A_List")
            {
                GameObject.Find("CureA").GetComponent<ItemDrag>().enabled = false;
                GameObject.Find("CureA").GetComponent<Image>().sprite = inactive;
            }
            if (gameObject.name == "B_List")
            {
                GameObject.Find("CureB").GetComponent<ItemDrag>().enabled = false;
                GameObject.Find("CureB").GetComponent<Image>().sprite = inactive;
            }
            if (gameObject.name == "C_List")
            {
                GameObject.Find("CureC").GetComponent<ItemDrag>().enabled = false;
                GameObject.Find("CureC").GetComponent<Image>().sprite = inactive;
            }
        }

        if (totalMeteors == 0)
            resourceBar.Find("Bar").GetComponent<RectTransform>().sizeDelta = new Vector2(0f, 37.5f);
        else if (totalMeteors == 1)
            resourceBar.Find("Bar").GetComponent<RectTransform>().sizeDelta = new Vector2(77f, 37.5f);
        else if (totalMeteors == 2)
            resourceBar.Find("Bar").GetComponent<RectTransform>().sizeDelta = new Vector2(177f, 37.5f);
        if (totalMeteors == 3)
        {
            resourceBar.Find("Bar").GetComponent<RectTransform>().sizeDelta = new Vector2(262.5f, 37.5f);
            gameObject.GetComponent<CureProcess>().enabled = true;
            this.enabled = false;
        }
    }
}
