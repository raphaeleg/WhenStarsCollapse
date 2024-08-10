using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.WSA;

public class MeteorMerge : MonoBehaviour, IDropHandler
{
    // Combinations database
    // Meteor1 + Meteor2 = Cure
    public string[,] combinations = {
        {"MeteorA", "MeteorA", "CureA"},
        {"MeteorA", "MeteorB", "CureB"},
        {"MeteorB", "MeteorB", "CureC"}
    };

    public void OnDrop(PointerEventData eventData)
    {
        GameObject dropped = eventData.pointerDrag;
        for (int i = 0; i < combinations.Length; i++)
        {
            if ((dropped.name == combinations[i, 0] && gameObject.name == combinations[i, 1])
                || (dropped.name == combinations[i, 1] && gameObject.name == combinations[i, 0]))
            {
                Debug.Log(combinations[i, 2]);
                GameObject cure = Instantiate(Resources.Load<GameObject>("Cures/"+combinations[i, 2]));
                cure.transform.SetParent(gameObject.transform.parent.parent.Find("CuresList"));
                cure.name = cure.name.Substring(0, cure.name.Length - 7);
                Destroy(dropped);
                Destroy(gameObject);
            }
        }
    }
}
