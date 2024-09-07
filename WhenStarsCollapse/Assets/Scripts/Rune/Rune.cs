using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Runes
{
    public class Rune : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private Image image;
        [SerializeField] Sprite active;
        [SerializeField] Sprite inactive;
        #region Event Listeners
        private Dictionary<string, Action<int>> SubscribedEvents;

        private void Awake()
        {
            SubscribedEvents = new() {
                { "SetCure_Red", Event_UpdateRune },
            };
        }

        private void OnEnable()
        {
            foreach (var pair in SubscribedEvents)
            {
                EventManager.StartListening(pair.Key, pair.Value);
            }
        }

        private void OnDisable()
        {
            foreach (var pair in SubscribedEvents)
            {
                EventManager.StopListening(pair.Key, pair.Value);
            }
        }
        #endregion
        private void Start()
        {
            image = gameObject.GetComponent<Image>();
        }
        public void Event_UpdateRune(int val)
        {
            if (val > 0)
            {
                image.sprite = active;
                return;
            }
            image.sprite = inactive;
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            //transform.SetParent(transform.root);
            //transform.SetAsLastSibling();
            image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            //transform.SetParent(parent);
            //transform.SetSiblingIndex(0);
            transform.SetLocalPositionAndRotation(new Vector3(-135, 0, 0), Quaternion.identity);
            image.raycastTarget = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = Vector3.one;
        }
    }
}