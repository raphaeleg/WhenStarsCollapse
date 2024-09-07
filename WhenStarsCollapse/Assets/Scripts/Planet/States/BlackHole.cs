using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meteors;

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

            if (other.CompareTag("Planet")) {
                other.GetComponent<Planet>().ShrinkUntilDestroy(Planet.gameObject); 
            }
            else if (other.GetComponent<Anim_Shrink>()) {
                other.GetComponent<Anim_Shrink>().ShrinkUntilDestroy(Planet.gameObject); 
            }
        }

        public override void ShrinkUntilDestroy(GameObject collider)
        {
            return;
        }
    }
}