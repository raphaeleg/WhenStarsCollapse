using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader sceneLoader;

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

    public static void LoadGameplay()
    {
        LoadScene("Gameplay");
    }

    public static void LoadLoseScreen()
    {
        LoadScene("Highscore");
    }

    public static void QuitApplication()
    {
        Application.Quit();
    }
}