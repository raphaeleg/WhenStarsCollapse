using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WhiteDwarf : State
{
    public WhiteDwarf(Planet planet) : base(planet) { }

    public int stage = 1;

    public override IEnumerator Start()
    {
        // highScore.whiteDwarfs++;
        // animator.SetTrigger("Dwarf");

        yield return new WaitForSeconds(20);

        /*transform.Find("Image").GetComponent<Animator>().enabled = false;
        transform.Find("Image").localPosition = new Vector3(9.2f, 16.4f, 0);
        transform.Find("Image").localScale = new Vector3(5, 5, 5);
        for (int i = 0; i < whiteDwardDestroyAnim.Length; i++)
        {
            transform.Find("Image").GetComponent<Image>().sprite = whiteDwardDestroyAnim[i];
            yield return new WaitForSeconds(0.05f);
        }*/
        Planet.OnDestroy();
    }
}
