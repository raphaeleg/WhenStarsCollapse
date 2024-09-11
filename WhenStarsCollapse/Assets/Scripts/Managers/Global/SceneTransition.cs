using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneTransition : MonoBehaviour
{
    private SceneTransition instance;
    private Animator animator;
    #region EventManager
    private Dictionary<string, Action<int>> SubscribedEvents;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        animator = GetComponent<Animator>();
        SubscribedEvents = new() {
            { "AnimateLoadScene", Load },
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
    public void Load(int duration)
    {
        StartCoroutine("Fading", duration * 0.1f);
    }
    private IEnumerator Fading(float time)
    {
        animator.SetTrigger("ExitScene");
        yield return new WaitForSeconds(time);
        animator.SetTrigger("EnterScene");
    }
}
