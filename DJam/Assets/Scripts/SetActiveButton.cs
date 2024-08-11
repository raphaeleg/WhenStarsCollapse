using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveButton : MonoBehaviour
{
    public GameObject[] toActive;
    public GameObject[] toNotActive;
    public AudioSource sfx;
    public bool getCard;

    // SetActive changes when button is clicked
    public void ButtonPressed()
    {
        //BackgroundMusic.instance.PlaySFX("SFX_Click_2");

        if (sfx != null)
        {
            sfx.Play();
        }

        for (int i = 0; i < toActive.Length; i++)
        {
            toActive[i].SetActive(true);
        }
        for (int i = 0; i < toNotActive.Length; i++)
        {
            toNotActive[i].SetActive(false);
        }
    }
}
