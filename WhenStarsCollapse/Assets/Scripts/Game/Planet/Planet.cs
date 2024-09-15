using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Planets
{
    /// <summary>
    /// A StateMachine that manages the current Planet State.
    /// Also holds a planet's visuals and faction.
    /// </summary>
    public class Planet : StateMachine
    {
        public Faction faction { get; private set; }
        public PlanetVisuals visuals;

        public void Start()
        {
            faction = GetComponent<Faction>();
            faction.SetRandom();
            SetState(new Begin(this));
        }

        public void ShrinkUntilDestroy(GameObject other)
        {
            State.ShrinkUntilDestroy(other);
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            StartCoroutine(State.Collided(other));
        }

        public void OnDestroy()
        {
            Destroy(gameObject);
        }
    }
}