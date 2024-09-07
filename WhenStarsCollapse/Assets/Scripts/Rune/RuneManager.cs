using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runes
{
    public class RuneManager : StateMachine
    {
        private int cures = 0;
        #region Event Listeners
        private Dictionary<string, Action<int>> SubscribedEvents;

        private void Awake()
        {
            SubscribedEvents = new() {
                { "AddCure_Red", Event_CalcCure },
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
        public void Event_CalcCure(int val)
        {
            cures += val;
            EventManager.TriggerEvent("SetCure_Red", cures);
        }
    }
}