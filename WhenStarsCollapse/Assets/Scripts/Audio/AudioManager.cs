using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            //Debug.LogError("Found more than one Audio Manager in scene");
            Destroy(gameObject);
        }
        instance = this;
    }

    public void PlayOneShot(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound, Vector3.zero);
    }
}
