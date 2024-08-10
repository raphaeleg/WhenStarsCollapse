using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlanetRuntimeSet : RuntimeSet<GameObject>
{
    public List<GameObject> Planets = new();

    public override void Add(GameObject p)
    {
        Planets.Add(p);
    }
    public override void Remove(GameObject p)
    {
        Planets.Remove(p);
    }
    public void Clear()
    {
        Planets.Clear();
    }
    public void AddSymptomToRandom()
    {
        int random = Random.Range(0, Planets.Count-1);
        Planets[random].GetComponent<Planet>().AddSymptom();
    }
}