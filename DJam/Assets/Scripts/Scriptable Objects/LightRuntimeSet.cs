using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LightRuntimeSet : RuntimeSet<LightSource>
{
    [SerializeField]
    private List<LightSource> AllyLights = new();
    [SerializeField]
    public List<LightSource> AllLights = new();

    public override void Add(LightSource light)
    {
        AllyLights.Add(light);
        AllLights.Add(light);
    }

    public override void Remove(LightSource light)
    {
        LightSource removeLight = AllyLights.Find(item => item.gameObject == light.gameObject);
        if (removeLight.gameObject != null) { AllyLights.Remove(removeLight); }
    }
    public void Clear()
    {
        AllyLights.Clear();
    }
}