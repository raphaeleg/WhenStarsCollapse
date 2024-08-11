using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] Audio sound;
    [SerializeField] bool use;

    public void Footstep()
    {
        if (!use) { return; }
        sound.Play(Sound.Name.Footstep);
    }

    public void Summon()
    {
        if (!use) { return; }
        sound.Play(Sound.Name.Summon);
    }
}
