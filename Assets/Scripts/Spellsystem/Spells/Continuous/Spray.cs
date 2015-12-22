using System;
using UnityEngine;

namespace Spellsystem
{

    /// <summary>
    /// Creates a Spray.
    /// This is the final version for all types of Sprays like a Fire Spray.
    /// If you want to create a specific spell like a Fire Spray you have to do this via the editor only. pls see the README.txt
    /// </summary>
    public sealed class Spray : SpellLogic, VFXHandler
    {

        ParticleSystem ps;

        protected override void Awake()
        {
            base.Awake();
            SpellInformation.Spelltype = SpellType.Continuous;
            SpellInformation.Spellform = SpellForm.Spray;
        }


        public override void ApplyEffect(GameObject gobject)
        {
            IStatusEffect effectReceiver = gobject.GetComponent(typeof(IStatusEffect)) as IStatusEffect;
            effectReceiver.RecvEffect(SpellInformation);
        }

        public override void Kill()
        {
            // Wanna add some visual effects here? Call DeathVisuals(). Be sure to implement it first.
            RaiseSpellExpired();
            Destroy(ps);
            Destroy(this.gameObject);
        }

        public void DeathVisuals()
        {
            // No death visuals here.
        }


        public void StartVisuals()
        { 
            ps = Instantiate(VFXPool.Instance.Sprays[SpellInformation.Elements[0].ToString()], transform.position, transform.rotation) as ParticleSystem;
            ps.transform.SetParent(transform);  
        }
    }
}