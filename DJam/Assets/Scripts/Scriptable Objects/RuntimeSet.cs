using UnityEngine;

public abstract class RuntimeSet<T> : ScriptableObject
{
    public abstract void Add(T item);
    public abstract void Remove(T item);
}