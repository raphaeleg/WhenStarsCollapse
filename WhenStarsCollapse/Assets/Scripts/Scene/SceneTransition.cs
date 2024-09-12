using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance { get; private set; }
    private Animator animator;
    #region EventManager
    private Dictionary<string, Action<int>> SubscribedEvents;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        instance.animator = GetComponent<Animator>();
        SubscribedEvents = new() {
            { "AnimateLoadScene", Load },
        };
    }
    private void OnEnable()
    {
        StartCoroutine("DelayedSubscription");
    }
    private IEnumerator DelayedSubscription()
    {
        yield return new WaitForSeconds(0.0001f);
        foreach (var pair in SubscribedEvents)
        {
            EventManager.StartListening(pair.Key, pair.Value);
        }
    }

    private void OnDisable()
    {
        if (SubscribedEvents == null) { return; }
        foreach (var pair in SubscribedEvents)
        {
            EventManager.StopListening(pair.Key, pair.Value);
        }
    }
    #endregion
    public void Load(int duration)
    {
        StartCoroutine("Fading", duration * 0.1f);
    }
    private IEnumerator Fading(float time)
    {
        instance.animator.SetTrigger("ExitScene");
        yield return new WaitForSeconds(time);
        instance.animator.SetTrigger("EnterScene");
    }
}
