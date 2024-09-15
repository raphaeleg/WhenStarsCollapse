using UnityEngine;

public enum CursorType { Arrow, Grab, Click }

/// <summary>
/// A Scriptable Object that maps a cursor animation to a CursorType.
/// </summary>
[CreateAssetMenu]
    public class CursorAnimation : ScriptableObject
{
    public CursorType cursorType;
    public bool playOnce = false;
    public Texture2D[] textureArray;
    public float frameRate = 0.1f;
    public Vector2 offset;
}