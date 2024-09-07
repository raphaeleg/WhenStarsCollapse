using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meteor 
{
    public class Meteor : MonoBehaviour
    {
        /*
        Meteor
        - Type Random
        - Flying around
        - Right orientation
        */
        public enum Type { BLUE, GREEN, RED };
        public Type type = Type.BLUE;
        private Animator animator;
        void Start()
        {
            animator = gameObject.GetComponent<Animator>();
            
        }

        void Update()
        {
            
        }
    }
}