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

    public void Start()
    {
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

    public void ShrinkUntilDestroy()
    {
        StartCoroutine(State.Shrink());
    }

    public void OnDestroy()
    {
        Destroy(gameObject);
    }

    public void StartSuckIn(GameObject collider) { StartCoroutine(SuckIn(collider)); }
    public IEnumerator SuckIn(GameObject collider)
    {
        float time = 0.5f;
        while (time > 0 && collider != null)
        {
            var directionToCollider = collider.transform.position - transform.position;
            var step = 5f * directionToCollider;
            collider.transform.localPosition -= step;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
