using System;
using System.Collections.Generic;
using UnityEngine;

namespace Runes
{
    public class MeteorBar : MonoBehaviour
    {
        private static int METEORS_FOR_CURE = 3;
        private int collectedMeteors = 0;
        private RectTransform _transform;

        private static float BAR_HEIGHT = 37.5f;
        private static float BAR_WIDTH_1 = 77f;
        private static float BAR_WIDTH_2 = 177f;
        private static float BAR_WIDTH_3 = 262.5f;

        #region Event Listeners
        private Dictionary<string, Action<int>> SubscribedEvents;

        private void Awake()
        {
            SubscribedEvents = new() {
                { "CollectMeteor_Red", Event_AddMeteor },
                { "SetCure_Red", Event_ResetMeteor },
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
            _transform = gameObject.GetComponent<RectTransform>();
            SetVisual();
        }
        public void Event_AddMeteor(int val)
        {
            collectedMeteors++;
            SetVisual();
            if (collectedMeteors == METEORS_FOR_CURE)
            {
                EventManager.TriggerEvent("ProcessCure_Red", 0);
            }
        }

        public void Event_ResetMeteor(int val)
        {
            if (val > 0) { 
                collectedMeteors = 0; 
                SetVisual();
            }
        }

        private void SetVisual()
        {
            _transform.sizeDelta = collectedMeteors switch
            {
                1 => new Vector2(BAR_WIDTH_1, BAR_HEIGHT),
                2 => new Vector2(BAR_WIDTH_2, BAR_HEIGHT),
                3 => new Vector2(BAR_WIDTH_3, BAR_HEIGHT),
                _ => new Vector2(0, BAR_HEIGHT),
            };
        }
    }
}