using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseUI;
    private bool paused;

    public void ButtonPressed()
    {
        paused = !paused;

        if (paused)
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
            BackgroundMusic.instance.PlaySFX("SFX_Click_2");
        }
        else
        {
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (paused)
            Time.timeScale = 0;
    }
}
