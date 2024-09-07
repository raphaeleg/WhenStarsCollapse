using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Planets
{
    public class PlanetVisuals : MonoBehaviour
    {
        private const int RATE_OF_BLACKHOLE_ROTATION = 10;
        private bool isRotating = false;

        private SpriteRenderer spriteRenderer;
        private Animator animatorMain;
        private Animator animatorOverlay;

        [SerializeField] Transform objectTransform;
        [SerializeField] BoxCollider2D collider2d;

        private void Start() {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            animatorMain = gameObject.GetComponent<Animator>();
            animatorOverlay = transform.parent.GetChild(0).gameObject.GetComponent<Animator>();
        }

        public void SetPlanetType(int t) {
            animatorMain.SetInteger("Type", t); // 0 = blue, 1 = green, 2 = red
        }

        private void Update() {
            if (isRotating) {transform.Rotate(0, 0, Time.deltaTime * RATE_OF_BLACKHOLE_ROTATION);}
        }

        public void Anim_Spawn(){
            animatorMain.SetTrigger("Spawn");
        }

        public void Anim_Sick() {
            animatorMain.SetTrigger("Sick");
        }
        public void Anim_GetBig() {
            animatorMain.SetTrigger("GetBig");
        }

        public void Anim_BecomeBlackHole() {
            Anim_Explode();
            isRotating = true;
            collider2d.size = new Vector2(1.5f, 1.5f);
        }
        public void Anim_Explode() {
            animatorMain.SetTrigger("Explode");
        }
        public void Anim_Eat() {
            animatorOverlay.SetTrigger("Eat");
        }
        public void Anim_Heal() {
            animatorOverlay.SetTrigger("Heal");
        }

        public void Anim_BecomeDwarf(){
            animatorMain.SetTrigger("Dwarf_Spawn");
        }
        public void Anim_DestroyDwarf(){
            animatorMain.SetTrigger("Dwarf_Destroy");
        }
    }
}