using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUp : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float power = 1.1f;
    private Vector3 origin;

    private void Start()
    {
        origin = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = origin * power;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = origin;
    }

}
