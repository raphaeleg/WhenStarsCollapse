using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public string[] curesNeeded;
    public bool[] curesAdded;

    private void Awake()
    {
        curesAdded = new bool[curesNeeded.Length];
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        for (int i = 0; i < curesNeeded.Length; i++)
        {
            if (curesNeeded[i] == dropped.name)
            {
                curesAdded[i] = true;
                break;
            }
        }
    }

    private void Update()
    {
        bool healed = true;
        Debug.Log(curesAdded.Length);
        for (int i = 0; i < curesAdded.Length; i++)
        {
            if (curesAdded[i] == false)
                healed = false;
        }
        if (healed)
            Destroy(gameObject);
    }
}
