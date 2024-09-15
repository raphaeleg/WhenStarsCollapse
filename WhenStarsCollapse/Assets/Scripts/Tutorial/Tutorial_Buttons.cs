using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

namespace Tutorial
{
    /// <summary>
    /// Button behaviours given the current Tutorial Dialogue shown.
    /// </summary>
    public class TutorialButtons : MonoBehaviour
    {
        public enum Type { BACK, NEXT };
        public Type button;
        private GameObject child;
        private Color YELLOW = new Color(0.945098f, 0.8901961f, 0.5333334f, 1);
        #region EventManager
        private Dictionary<string, Action<int>> SubscribedEvents;

        private void Awake()
        {
            child = transform.GetChild(0).gameObject;
            SubscribedEvents = new() {
                { "Tutorial_HideBackBtn", Event_HideBtn },
                { "Tutorial_ReadyNextBtn", Event_ReadyBtn },
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
            if (button == Type.BACK) 
            { 
                EventManager.TriggerEvent("Tutorial_Next", -1); 
            }
            else 
            { 
                EventManager.TriggerEvent("Tutorial_Next", 1); 
            }
        }
        public void Event_HideBtn(int val)
        {
            if (button != Type.BACK) 
            { 
                return; 
            }
            child.SetActive(val != 0);
        }
        public void Event_ReadyBtn(int val)
        {
            if (button != Type.NEXT) 
            { 
                return; 
            }
            TMP_Text label = child.transform.GetComponentsInChildren<TMP_Text>()[0];
            label.text = val == 1 ? "Next" : "Start";
            label.color = val == 1 ? Color.white : YELLOW;
        }
    }
}