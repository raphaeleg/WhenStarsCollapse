using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Runes
{
    public class RuneCount : MonoBehaviour
    {
        private TMP_Text text;
        #region Event Listeners
        private Dictionary<string, Action<int>> SubscribedEvents;

        private void Awake()
        {
            SubscribedEvents = new() {
                { "SetCure_Red", Event_UpdateText },
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
            text = gameObject.GetComponent<TMP_Text>();
        }
        public void Event_UpdateText(int val)
        {
            text.text = val.ToString();
        }
    }
}