using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runes
{

    public class RuneProcess : MonoBehaviour
    {
        private RuneManager parent;
        #region Event Listeners
        private Dictionary<string, Action<int>> SubscribedEvents;

        private void Awake()
        {
            parent = transform.parent.GetComponent<RuneManager>();
            SubscribedEvents = new() {
                { parent.TypeToString("ProcessCure"), Event_StartProcess },
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
        public void Event_StartProcess(int val)
        {
            StartCoroutine("Running");
        }

        private IEnumerator Running()
        {
            for (int i = 0; i < 25; i++)
            {
                transform.localScale = new Vector3((i * 4 + 1) / 100f, 1f, 1f);
                yield return new WaitForSeconds(0.01f);
            }
            transform.localScale = new Vector3(0f, 1f, 1f);
            EventManager.TriggerEvent(parent.TypeToString("AddCure"), 1);
        }
    }
}