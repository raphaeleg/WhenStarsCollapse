using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Planet : StateMachine
{
    public enum Type { BLUE, GREEN, RED };
    public Type type = Type.BLUE;
    public PlanetVisuals visuals;
    
    public static Dictionary<Type, string> cureMap = new()
        {
            { Type.RED, "CureA" },
            { Type.GREEN, "CureB" },
            { Type.BLUE, "CureC" }
        };
    private bool IsRightCure(string item) { return item == cureMap[type]; }
    private bool triggeredShrink = false;

    public void Start()
    {
        int typesLength = System.Enum.GetValues(typeof(Type)).Length;
        type = (Type)Random.Range(0,typesLength);
        SetState(new Begin(this));
    }
    public void Update()
    {
        StartCoroutine(State.Update());
    }
    public virtual IEnumerator OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        if (!IsRightCure(dropped.name)) { yield break; }

        // TODO: Minus from runebar list
        StartCoroutine(State.Heal()); 
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        State.Collided(other);
    }

    public void ShrinkUntilDestroy(GameObject collider)
    {
        if (triggeredShrink) { return; }
        triggeredShrink = true;

        StartCoroutine(State.Shrink(collider));
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }
}
