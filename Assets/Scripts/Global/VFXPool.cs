using UnityEngine;
using System.Collections.Generic;

namespace Spellsystem
{
    public class VFXPool : MonoBehaviour
    {
        public static VFXPool Instance { get; private set; }

        public Dictionary<string, ParticleSystem> Sprays;


        void Awake()
        {
            if(Instance != null && Instance != this )
            {
                Destroy(gameObject);
            }

            Instance = this;
        }

    }
}