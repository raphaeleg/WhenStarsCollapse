using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Runes
{
    public class Rune : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public Faction faction { get; private set; }
        private Image image;
        private bool isActive = false;
        private bool isDrag = false;
        private static Vector3 OG_POSITION = new Vector3(-135, 0, 0);
        private static Vector3 SCALE = new Vector3(1.2f, 1.2f, 1.2f);
        [SerializeField] Sprite active;
        [SerializeField] Sprite inactive;
        #region Event Listeners
        private Dictionary<string, Action<int>> SubscribedEvents;
        private void Awake()
        {
            faction = transform.parent.GetComponent<Faction>();
            SubscribedEvents = new() {
                { faction.StringType("SetCure"), Event_UpdateRune },
                { faction.StringType("AddCure"), Event_CureUsed },
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
            isActive = val > 0;
            image.sprite = isActive ? active : inactive;
        }
        public void Event_CureUsed(int val)
        {
            if (val > 0) { return; }
            EndDrag();
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!isActive) { return; }
            image.raycastTarget = false;
            isDrag = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isDrag || !isActive) { return; }
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!isActive) { return; }
            EndDrag();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!isActive) { return; }
            transform.localScale = SCALE;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = Vector3.one;
        }

        public void EndDrag()
        {
            isDrag = false;
            transform.SetLocalPositionAndRotation(OG_POSITION, Quaternion.identity);
            image.raycastTarget = true;
        }
    }
}