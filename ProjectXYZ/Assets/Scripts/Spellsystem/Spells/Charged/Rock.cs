using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Spellsystem
{
    public sealed class Rock : SpellLogic
    {

        protected override void Awake()
        {
            base.Awake();
            Damage.Spellform = SpellForm.Rock;
            Spellform = SpellForm.Rock;
            Spelltype = SpellType.Charged;
            vfx.StartVisuals();
            GetComponent<SpellMovement>().StartMovement(Damage.ChargeTime);            
        }

        public override void ApplyEffect(GameObject gobject)
        {
            IStatusEffect effectReceiver = gobject.GetComponent(typeof(IStatusEffect)) as IStatusEffect;
            effectReceiver.RecvEffect(Damage, Spelltype); 
        }

        public override void Kill()
        {
            RaiseSpellExpired();
            vfx.DeathVisuals();
            Destroy(this.gameObject);
        }

    }
}
