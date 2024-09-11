using System.Collections.Generic;
using System;
using UnityEngine;

namespace Tutorial
{
    public class TutorialButtons : MonoBehaviour
    {
        public enum Type { BACK, NEXT };
        public Type button;
        private GameObject child;
        #region EventManager
        private Dictionary<string, Action<int>> SubscribedEvents;

        private void Awake()
        {
            child = transform.GetChild(0).gameObject;
            SubscribedEvents = new() {
                { "Tutorial_HideBackBtn", Event_HideBtn }
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
        public void Next()
        {
            if (button == Type.BACK) { EventManager.TriggerEvent("Tutorial_Next", -1); }
            else { EventManager.TriggerEvent("Tutorial_Next", 1); }
        }
        public void Event_HideBtn(int val)
        {
            if (button != Type.BACK) { return; }
            child.SetActive(val != 0);
        }
    }
}