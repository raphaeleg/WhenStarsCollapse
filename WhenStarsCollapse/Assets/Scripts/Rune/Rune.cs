using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Runes
{
    public class Rune : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        private Faction faction;
        private Image image;
        [SerializeField] Sprite active;
        [SerializeField] Sprite inactive;
        #region Event Listeners
        private Dictionary<string, Action<int>> SubscribedEvents;
        private void Awake()
        {
            faction = transform.parent.GetComponent<Faction>();
            SubscribedEvents = new() {
                { faction.StringType("SetCure"), Event_UpdateRune },
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
            image.raycastTarget = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
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

        public new string GetType()
        {
            return faction.StringType();
        }
    }
}