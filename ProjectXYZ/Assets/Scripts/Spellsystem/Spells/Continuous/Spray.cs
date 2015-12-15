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

        protected override void Awake()
        {
            base.Awake();
            Spelltype = SpellType.Continuous;
            Spellform = SpellForm.Spray;
            Damage.Spellform = Spellform;
        }


        public override void ApplyEffect(GameObject gobject)
        {
            IStatusEffect effectReceiver = gobject.GetComponent(typeof(IStatusEffect)) as IStatusEffect;
            effectReceiver.RecvEffect(Damage, Spelltype);
        }

        public override void Kill()
        {
            // Wanna add some visual effects here? 
            RaiseSpellExpired();
            Destroy(this.gameObject);
        }

        public void DeathVisuals()
        {
            
        }

        public void StartVisuals()
        {
            
        }
    }
}