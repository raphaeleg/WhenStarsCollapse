using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meteors
{
    public class Meteor : MonoBehaviour
    {
        /*
        Meteor
        - Right orientation
        */
        public enum Type { BLUE, GREEN, RED };
        public Type type = Type.BLUE;
        private Animator animator;
        Vector3 direction = Vector3.zero;
        private static float SPEED = 5f;
        
        public void Init(Vector3 pos, Vector3 dir) 
        {
            transform.position = pos;
            direction = dir;
            Debug.Log("Spawned with "+pos+" "+dir);
        }

        private void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            SetType();
        }

        private void Update()
        {
            transform.Translate(direction * SPEED * Time.deltaTime); 
        }

        private void SetType()
        {
            int typesLength = System.Enum.GetValues(typeof(Type)).Length;
            type = (Type)Random.Range(0,typesLength);
        }

        private void OnTriggerExit2D(Collider2D other) {
            if (other.CompareTag("MeteorSpawnArea")) {
                Destroy(gameObject);
            }
        }
    }
}