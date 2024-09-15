using System.Collections;
using UnityEngine;

namespace Planets
{
    /// <summary>
    /// Handles the visuals of a Planet (Particles & Animations)
    /// </summary>
    public class PlanetVisuals : MonoBehaviour
    {
        private const int RATE_OF_BLACKHOLE_ROTATION = 10;
        private bool isRotating = false;

        private Animator animatorMain;
        private Animator animatorOverlay;
        private int type = 0;

        [SerializeField] Transform objectTransform;
        [SerializeField] BoxCollider2D collider2d;

        [SerializeField] GameObject Particle_Heal;
        [SerializeField] GameObject Particle_Red;
        [SerializeField] GameObject Particle_Green;
        [SerializeField] GameObject Particle_Blue;

        private void Start()
        {
            animatorMain = gameObject.GetComponent<Animator>();
            animatorOverlay = transform.parent.GetChild(0).gameObject.GetComponent<Animator>();
        }

        public void SetPlanetType(int t)
        {
            type = t;
            animatorMain.SetInteger("Type", t); // 0 = blue, 1 = green, 2 = red
        }

        private void Update()
        {
            if (isRotating) 
            { 
                transform.Rotate(0, 0, Time.deltaTime * RATE_OF_BLACKHOLE_ROTATION); 
            }
        }

        public void Anim_Spawn()
        {
            animatorMain.SetTrigger("Spawn");
        }

        public void Anim_Sick()
        {
            animatorMain.SetTrigger("Sick");
        }
        public void SickParticle(bool enable)
        {
            switch (type)
            {
                case 0:
                    Particle_Blue.SetActive(enable);
                    break;
                case 1:
                    Particle_Green.SetActive(enable);
                    break;
                default:
                    Particle_Red.SetActive(enable);
                    break;
            }
        }
        public void Anim_GetBig()
        {
            animatorMain.SetTrigger("GetBig");
        }

        public void Anim_BecomeBlackHole()
        {
            Anim_Explode();
            isRotating = true;
            collider2d.size = new Vector2(1.5f, 1.5f);
        }
        public void Anim_Explode()
        {
            animatorMain.SetTrigger("Explode");
        }
        public void Anim_Eat()
        {
            animatorOverlay.SetTrigger("Eat");
        }
        public void Anim_Heal(float duration)
        {
            SickParticle(false);
            StartCoroutine("HealParticles", duration);
        }

        private IEnumerator HealParticles(float duration)
        {
            Particle_Heal.SetActive(true);
            yield return new WaitForSeconds(duration);
            Particle_Heal.SetActive(false);
        }

        public void Anim_BecomeDwarf()
        {
            animatorMain.SetTrigger("Dwarf_Spawn");
        }
        public void Anim_DestroyDwarf()
        {
            animatorMain.SetTrigger("Dwarf_Destroy");
        }
    }
}