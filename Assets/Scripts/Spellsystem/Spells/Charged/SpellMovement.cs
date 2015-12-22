using System;
using UnityEngine;

namespace Spellsystem
{
    /// <summary>
    /// Handles the movement of a spell.
    /// </summary>
    /// 
    [RequireComponent(typeof(Rigidbody))]
    public class SpellMovement : MonoBehaviour
    {
        Rigidbody rb;
        public float Speed;

        /// <summary>
        /// The Y component of an impuls
        /// </summary>
        public float HeightImpuls;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }


        public void StartMovement(float input)
        {
            Vector3 toAdd = transform.forward * Speed * input;
            toAdd.y = HeightImpuls * input;
            rb.AddForce(toAdd, ForceMode.Impulse);    
        }
    }
}