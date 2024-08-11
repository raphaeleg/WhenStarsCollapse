using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Sound
{
    public enum Name { Footstep, Summon, Focus, Monster, GameWin, Soundtrack }

    public static string AudioEnumToName(Name name)
    {
        return name switch
        {
            Name.Footstep => "Footstep",
            Name.Summon => "Summon",
            Name.Focus => "Focus",
            Name.Monster => "Monster",
            Name.GameWin => "Game Win",
            Name.Soundtrack => "Soundtrack",
            _ => ""
        };
    }
}

public class Audio : MonoBehaviour
{
    [Tooltip("Find the AudioSource component in its children and store them in this list.")]
    public List<AudioSource> audioList;

    void Start()
    {
        List<GameObject> audioObjects = new();
        //Method.GetChildRecursive(gameObject, audioObjects, "");

        foreach (GameObject obj in audioObjects) 
        {
            if (obj.TryGetComponent<AudioSource>(out var audioSource)) { audioList.Add(audioSource); }
        }
    }

    public void Play(Sound.Name name)
    {
        int index = GetIndex(name);
        audioList[index].PlayOneShot(audioList[index].clip);
    }

    public void Volume(Sound.Name name, float vol)
    {
        int index = GetIndex(name);
        audioList[index].volume = vol;
    }
    private int GetIndex(Sound.Name name)
    {
        string audioName = Sound.AudioEnumToName(name);
        return AudioNameToIndex(audioName);
    }
    private int AudioNameToIndex(string name)
    {
        for (int i = 0; i < audioList.Count; i++)
        {
            if (audioList[i].gameObject.name == name)
                return i; // The index
        }
        return -1; // It is not in the list
    }
}