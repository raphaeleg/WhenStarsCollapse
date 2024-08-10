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
}