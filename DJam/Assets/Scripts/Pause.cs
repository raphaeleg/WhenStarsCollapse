using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pauseUI;
    public bool paused;

    public Pause other1;
    public Pause other2;

    public Sprite imagePause;
    public Sprite imageNotPause;

    public void ButtonPressed()
    {
        paused = !paused;

        if (paused)
        {
            if (other1.paused)
                other1.ButtonPressed();
            if (other2.paused)
                other2.ButtonPressed();
            Time.timeScale = 0;
            pauseUI.SetActive(true);
            //BackgroundMusic.instance.PlaySFX("SFX_Click_2");
            gameObject.GetComponent<Image>().sprite = imagePause;
        }
        else
        {
            Time.timeScale = 1;
            pauseUI.SetActive(false);
            //BackgroundMusic.instance.PlaySFX("SFX_Click_1");
            gameObject.GetComponent<Image>().sprite = imageNotPause;
        }
    }

    private void Update()
    {
        if (paused)
            Time.timeScale = 0;
    }
}
