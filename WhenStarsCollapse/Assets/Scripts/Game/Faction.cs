using UnityEngine;

/// <summary>
/// Attached to an object, defines which faction it belongs to, for inner-faction interactions and events.
/// </summary>
public class Faction : MonoBehaviour
{
    public enum Type { BLUE, GREEN, RED };
    public Type type;
    public Faction(Type t)
    {
        this.type = t;
    }
    public void SetRandom()
    {
        int typesLength = System.Enum.GetValues(typeof(Type)).Length;
        int rndInt = UnityEngine.Random.Range(0, typesLength);
        SetType((Type)rndInt);
    }
    public void SetType(Type t)
    {
        type = t;
    }
    public int IntType()
    {
        return (int)type;
    }
    public string StringType()
    {
        return type switch
        {
            Type.RED => "Red",
            Type.GREEN => "Green",
            Type.BLUE => "Blue",
            _ => "Unknown",
        };
    }
    public string StringType(string prefix)
    {
        return prefix + "_" + StringType();
    }
    public bool CompareType(Faction other) { return type == other.type; }
}