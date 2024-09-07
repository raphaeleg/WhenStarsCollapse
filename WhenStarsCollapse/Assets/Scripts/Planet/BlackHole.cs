using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planets
{
    public class BlackHole : State
    {
        public BlackHole(Planet planet) : base(planet) { }

        public override IEnumerator Start()
        {
            Planet.visuals.Anim_BecomeBlackHole();
            yield return new WaitForSeconds(0.5f);

            EventManager.TriggerEvent("blackHoleSpawn", 0);
        }
        public override void Collided(Collider2D other)
        {
            /*var blackHoleEat = Instantiate(BlackholeOverlay);
            blackHoleEat.GetComponent<AnimationNoLoop>().destroySelf = true;
            blackHoleEat.transform.SetParent(transform);
            blackHoleEat.transform.localScale = new Vector3(2, 2, 2);
            blackHoleEat.transform.localPosition = Vector3.zero;*/

            GameObject collider = other.gameObject;
            if (other.GetComponent<Planet>()!= null) { other.GetComponent<Planet>().ShrinkUntilDestroy(Planet.gameObject); }
            //else { collider.GetComponent<TakeItem>().ShrinkUntilDestroy(); }
        }

        

        public override IEnumerator Shrink(GameObject collider)
        {
            yield break;
        }
    }
}