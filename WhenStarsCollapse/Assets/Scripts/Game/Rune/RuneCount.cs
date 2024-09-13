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
            Faction faction = transform.parent.GetComponent<Faction>();
            text = gameObject.GetComponent<TMP_Text>();
            SubscribedEvents = new() {
                { faction.StringType("SetCure"), Event_UpdateText },
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
        public void Event_UpdateText(int val)
        {
            text.text = val.ToString();
        }
    }
}