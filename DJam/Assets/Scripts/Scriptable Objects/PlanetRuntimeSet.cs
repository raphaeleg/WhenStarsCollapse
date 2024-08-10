using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlanetRuntimeSet : RuntimeSet<Planet>
{
    [SerializeField] public List<Planet> Planets = new();

    public override void Add(Planet p)
    {
        Planets.Add(p);
    }
    public override void Remove(Planet p)
    {
        Planets.Remove(p);
    }
    public void Clear()
    {
        Planets.Clear();
    }
}