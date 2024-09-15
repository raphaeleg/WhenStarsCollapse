using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    /// <summary>
    /// Plays a certain Tutorial Screen Animation based on the current Tutorial Dialogue shown.
    /// </summary>
    public class Tutorial_Screen : MonoBehaviour
    {
        private Animator _animator;
        #region EventManager
        private Dictionary<string, Action<int>> SubscribedEvents;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            SubscribedEvents = new() {
                { "Tutorial_SetCurrentDialogue", Event_ChangeAnimation }
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
        public void Event_ChangeAnimation(int val)
        {
            _animator.SetInteger("CurrentScreen", val);
            _animator.SetTrigger("ChangeScreen");
        }
    }
}