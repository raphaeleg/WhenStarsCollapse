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
        private Animator animator;
        [SerializeField] Transform objectTransform;
        [SerializeField] BoxCollider2D collider2d;

        private void Start() {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            animator = gameObject.GetComponent<Animator>();
        }

        public void SetPlanetType(int t) {
            animator.SetInteger("Type", t); // 0 = blue, 1 = green, 2 = red
        }

        private void Update() {
            if (isRotating) {transform.Rotate(0, 0, Time.deltaTime * RATE_OF_BLACKHOLE_ROTATION);}
        }

        public void Anim_Spawn(){
            animator.SetTrigger("Spawn");
        }

        public void Anim_Sick() {
            animator.SetTrigger("Sick");
        }
        public void Anim_GetBig() {
            animator.SetTrigger("GetBig");
        }

        public void Anim_BecomeBlackHole() {
            Anim_Explode();
            isRotating = true;
            collider2d.size = new Vector2(1.5f, 1.5f);
        }
        public void Anim_Explode() {
            animator.SetTrigger("Explode");
        }

        public void Anim_BecomeDwarf(){
            animator.SetTrigger("Dwarf_Spawn");
        }
        public void Anim_DestroyDwarf(){
            animator.SetTrigger("Dwarf_Destroy");
        }
    }
}