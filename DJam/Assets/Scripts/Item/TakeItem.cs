using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TakeItem : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        // sfx.Play();
        transform.SetParent(GameObject.Find("Items").transform);
        gameObject.GetComponent<ItemDrag>().enabled = true;
        this.enabled = false;
    }
}
