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
            // Wanna add some visual effects here? Call DeathVisuals(). Be sure to implement it first.
            RaiseSpellExpired();
            Destroy(this.gameObject);
        }

        public void DeathVisuals()
        {
            // No death visuals here.
        }

        public void StartVisuals()
        {
            bool customSpell = false;
            //Decide here if you need a custom Spell Particles here, e.g. in MWW QFF is a steam spray, not a Water + Fire spray.
            /*Start defining



            End */
            
            if (!customSpell)
            {
                ParticleSystem first = Instantiate(VFXPool.Instance.Sprays[Damage.Elements[0].ToString()], transform.position, transform.rotation) as ParticleSystem;
                ParticleSystem second = Instantiate(VFXPool.Instance.Sprays[Damage.Elements[1].ToString()], transform.position, transform.rotation) as ParticleSystem;

                first.transform.SetParent(transform);
                second.transform.SetParent(transform);
            }

            if (customSpell)
            {
                ParticleSystem ps = Instantiate(VFXPool.Instance.Sprays[Damage.Elements[0].ToString() + Damage.Elements[1].ToString()], transform.position, transform.rotation) as ParticleSystem;
                ps.transform.SetParent(transform);
            }
            
        }
    }
}