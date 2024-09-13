using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader Instance;
    private const int DURATION = 5; 
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
        SubscribedEvents = new() {
            { "Lose", LoadLoseScreen },
            { "LoadGameplay", LoadGameplay },
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
        foreach (var pair in SubscribedEvents)
        {
            EventManager.StopListening(pair.Key, pair.Value);
        }
    }
    #endregion

    public static void LoadScene(string name)
    {
        Time.timeScale = 1f; // Always load scene with timescale 1
        EventManager.TriggerEvent("AnimateLoadScene", DURATION);
        Debug.Log("LOADING " + name); // there are MULTIPLE InstanceS OF GAMEPLAY AND OTHERS. 
        Instance.StartCoroutine(Transition(name));
    }
    public static IEnumerator Transition(string name)
    {
        yield return new WaitForSeconds(DURATION*0.1f);
        SceneManager.LoadScene(name);
    }
    public static void LoadTutorial()
    {
        LoadScene("Tutorial");
    }

    public static void LoadMainMenu()
    {
        LoadScene("MainMenu");
    }

    public static void LoadGameplay(int val)
    {
        LoadScene("Gameplay");
    }

    public static void LoadLoseScreen(int val)
    {
        LoadScene("Highscore");
    }

    public static void QuitApplication()
    {
        Application.Quit();
    }
}