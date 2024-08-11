using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using AK.Wwise;
using System.Xml.Linq;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        StartCoroutine(Running("DragAndDrop"));
    }
    public void LoadTutorialScene()
    {
        StartCoroutine(Running("Tutorial"));
    }
    public void LoadGameOverScene()
    {
        StartCoroutine(Running("WinScreen"));
    }
    public void LoadMenuScene()
    {
        StartCoroutine(Running("MainMenu"));
    }
    IEnumerator Running(string sceneName)
    {
        BackgroundMusic.instance.PlaySFX("SFX_Click_2");
        GameObject.Find("Canvas - Transition").GetComponent<SceneTransition>().PlayTransition();
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(sceneName);
        if (sceneName == "DragAndDrop" || sceneName == "MainMenu")
            BackgroundMusic.instance.AudioSettingsSetup();
    }
    public void Quit()
    {
        BackgroundMusic.instance.PlaySFX("SFX_Click_2");
        Application.Quit();
    }
}
