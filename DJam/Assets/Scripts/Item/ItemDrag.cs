using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Image image;
    private Transform parent;
    private AudioSource sfx;

    private void Start()
    {
        image = GetComponent<Image>();
        parent = transform.parent;
        sfx = GetComponent<AudioSource>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // sfx.Play();
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // sfx.Play();
        transform.SetParent(parent);
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        image.raycastTarget = true;
    }
}
