using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CureStat : MonoBehaviour
{
    public int totalMeteors = 0;
    public int totalCures = 0;
    private Transform resourceBar;
    private TMP_Text text;

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
                GameObject.Find("CureA").GetComponent<ItemDrag>().enabled = true;
            if (gameObject.name == "B_List")
                GameObject.Find("CureB").GetComponent<ItemDrag>().enabled = true;
            if (gameObject.name == "C_List")
                GameObject.Find("CureC").GetComponent<ItemDrag>().enabled = true;
        }
        else
        {
            if (gameObject.name == "A_List")
                GameObject.Find("CureA").GetComponent<ItemDrag>().enabled = false;
            if (gameObject.name == "B_List")
                GameObject.Find("CureB").GetComponent<ItemDrag>().enabled = false;
            if (gameObject.name == "C_List")
                GameObject.Find("CureC").GetComponent<ItemDrag>().enabled = false;
        }

        if (totalMeteors == 0)
        {
            resourceBar.Find("Bar1").gameObject.SetActive(false);
            resourceBar.Find("Bar2").gameObject.SetActive(false);
            resourceBar.Find("Bar3").gameObject.SetActive(false);
        }
        else if (totalMeteors == 1)
        {
            resourceBar.Find("Bar1").gameObject.SetActive(true);
            resourceBar.Find("Bar2").gameObject.SetActive(false);
            resourceBar.Find("Bar3").gameObject.SetActive(false);
        }
        else if (totalMeteors == 2)
        {
            resourceBar.Find("Bar1").gameObject.SetActive(true);
            resourceBar.Find("Bar2").gameObject.SetActive(true);
            resourceBar.Find("Bar3").gameObject.SetActive(false);
        }
        if (totalMeteors == 3)
        {
            resourceBar.Find("Bar1").gameObject.SetActive(true);
            resourceBar.Find("Bar2").gameObject.SetActive(true);
            resourceBar.Find("Bar3").gameObject.SetActive(true);
            gameObject.GetComponent<CureProcess>().enabled = true;
            this.enabled = false;
        }
    }
}
