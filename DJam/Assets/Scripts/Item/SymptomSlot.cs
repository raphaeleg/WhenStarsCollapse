using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SymptomSlot : MonoBehaviour, IDropHandler
{
    public string curesNeeded;

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (curesNeeded == dropped.name)
        {
            Destroy(dropped);
            Destroy(gameObject);
        }
    }
}
