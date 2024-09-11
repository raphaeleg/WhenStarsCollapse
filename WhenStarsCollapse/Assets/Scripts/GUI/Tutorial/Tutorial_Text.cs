using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class Tutorial_Text : MonoBehaviour
    {
        public List<string> TutorialText = new List<string>();
        private int currentScreen = 0;
        private TypeWriterEffect typewriter;
        #region EventManager
        private Dictionary<string, Action<int>> SubscribedEvents;

        private void Awake()
        {
            typewriter = GetComponent<TypeWriterEffect>();
            SubscribedEvents = new() {
            { "Tutorial_Next", Event_ChangeText }
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
            typewriter.Restart(TutorialText[currentScreen]);
        }
        public void Event_ChangeText(int val)
        {
            if (currentScreen + val > TutorialText.Count) { return; }
            currentScreen += val;
            typewriter.Restart(TutorialText[currentScreen]);
        }
    }
}