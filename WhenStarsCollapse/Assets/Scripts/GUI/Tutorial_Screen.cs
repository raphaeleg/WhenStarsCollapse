using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Screen : MonoBehaviour
{
    private Animator _animator;
    #region EventManager
    private Dictionary<string, Action<int>> SubscribedEvents;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        SubscribedEvents = new() {
            { "Tutorial_Next", Event_ChangeAnimation }
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
    public void Event_ChangeAnimation(int step)
    {
        int newScreen = _animator.GetInteger("CurrentScreen") + step;
        _animator.SetInteger("CurrentScreen", newScreen);
        _animator.SetTrigger("ChangeScreen");
    }
}
