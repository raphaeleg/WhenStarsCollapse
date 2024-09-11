using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader sceneLoader;
    #region EventManager
    private Dictionary<string, Action<int>> SubscribedEvents;

    private void Awake()
    {
        SubscribedEvents = new() {
            { "Lose", LoadLoseScreen },
            { "LoadGameplay", LoadGameplay },
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
    public static SceneLoader instance
    {
        get
        {
            if (!sceneLoader)
            {
                sceneLoader = FindObjectOfType(typeof(SceneLoader)) as SceneLoader;

                if (!sceneLoader)
                {
                    Debug.LogError("There needs to be one active SceneLoader script on a GameObject in your scene.");
                }
            }

            return sceneLoader;
        }
    }

    public static void LoadScene(string name)
    {
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