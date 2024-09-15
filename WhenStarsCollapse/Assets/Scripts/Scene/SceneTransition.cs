using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An animation that Fades in and out of scenes.
/// </summary>
public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance { get; private set; }
    private Animator animator;
    #region EventManager
    private Dictionary<string, Action<int>> SubscribedEvents;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Instance.animator = GetComponent<Animator>();
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
        Instance.animator.SetTrigger("ExitScene");
        yield return new WaitForSeconds(time);
        Instance.animator.SetTrigger("EnterScene");
    }
}
