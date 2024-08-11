using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayAudioSettingVisibility : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 1)
            gameObject.SetActive(false);
    }
}
