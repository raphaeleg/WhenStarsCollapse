using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 origin;

    private void Start()
    {
        origin = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = origin * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = origin;
    }

}
