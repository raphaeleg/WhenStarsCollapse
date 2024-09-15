using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    /// <summary>
    /// Manages the tutorial text by listening to the needed trigger to start the TypeWriter effect.
    /// </summary>
    public class Tutorial_Text : MonoBehaviour
    {
        public List<string> TutorialText = new List<string>();
        private TypeWriterEffect typewriter;
        #region EventManager
        private Dictionary<string, Action<int>> SubscribedEvents;

        private void Awake()
        {
            typewriter = GetComponent<TypeWriterEffect>();
            SubscribedEvents = new() {
                { "Tutorial_SetCurrentDialogue", Event_ChangeText }
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
        public void Start()
        {
            EventManager.TriggerEvent("Tutorial_DialogueCount", TutorialText.Count);
            typewriter.Restart(TutorialText[0]);
        }
        public void Event_ChangeText(int val)
        {
            typewriter.Restart(TutorialText[val]);
        }
    }
}