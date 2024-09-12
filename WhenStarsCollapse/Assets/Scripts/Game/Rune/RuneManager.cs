using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runes
{

    public class RuneManager : StateMachine
    {
        private Faction faction;
        [SerializeField] int cures = 0;
        #region Event Listeners
        private Dictionary<string, Action<int>> SubscribedEvents;

        private void Awake()
        {
            faction = GetComponent<Faction>();
            SubscribedEvents = new() {
                { faction.StringType("AddCure"), Event_CalcCure },
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
            EventManager.TriggerEvent(faction.StringType("SetCure"), cures);
        }
        public void Event_CalcCure(int val)
        {
            cures += val;
            EventManager.TriggerEvent(faction.StringType("SetCure"), cures);
        }
    }
}