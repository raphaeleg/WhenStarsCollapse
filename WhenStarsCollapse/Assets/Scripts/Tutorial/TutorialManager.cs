using System;
using System.Collections;
using System.Collections.Generic;
using Tutorial;
using UnityEngine;

namespace Tutorial { 
    public class TutorialManager : MonoBehaviour
    {
        private int currentDialogue = 0;
        private int dialogueCount = 0;
        private bool startGame = false;
        #region EventManager
        private Dictionary<string, Action<int>> SubscribedEvents;

        private void Awake()
        {
            SubscribedEvents = new() {
                { "Tutorial_Next", Event_SetTutorialDialogue },
                { "Tutorial_DialogueCount", Event_SetDialogueCount }
            };
        }
        private void OnEnable()
        {
            currentDialogue = 0;
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
        public void Event_SetDialogueCount(int val)
        {
            dialogueCount = val;
        }
        public void Event_SetTutorialDialogue(int val)
        {
            int nextDialogue = currentDialogue + val;

            if (nextDialogue < 0) { return; }
            if (nextDialogue >= dialogueCount && !startGame) {
                startGame = true;
                EventManager.TriggerEvent("LoadGameplay", 0);
                return;
            }

            if (nextDialogue == 0) { EventManager.TriggerEvent("Tutorial_HideBackBtn", 0); }
            else if (nextDialogue == 1) { EventManager.TriggerEvent("Tutorial_HideBackBtn", 1); }
            else if (nextDialogue == dialogueCount - 1) { EventManager.TriggerEvent("Tutorial_ReadyNextBtn", 0); }
            else if (nextDialogue == dialogueCount - 2) { EventManager.TriggerEvent("Tutorial_ReadyNextBtn", 1); }

            currentDialogue = nextDialogue;
            EventManager.TriggerEvent("Tutorial_SetCurrentDialogue", currentDialogue);
        }
    }
}
