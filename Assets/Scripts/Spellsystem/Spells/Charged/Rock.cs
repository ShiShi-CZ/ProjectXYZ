using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Spellsystem
{
    public sealed class Rock : SpellLogic, VFXHandler
    {

        protected override void Awake()
        {
            base.Awake();
            SpellInformation.Spellform = SpellForm.Rock;
            SpellInformation.Spelltype = SpellType.Charged;
            StartVisuals();
            GetComponent<SpellMovement>().StartMovement(SpellInformation.ChargeTime);            
        }

        public override void ApplyEffect(GameObject gobject)
        {
            IStatusEffect effectReceiver = gobject.GetComponent(typeof(IStatusEffect)) as IStatusEffect;
            effectReceiver.RecvEffect(SpellInformation); 
        }

        public override void Kill()
        {
            RaiseSpellExpired();
            DeathVisuals();
            Destroy(this.gameObject);
        }

        public void DeathVisuals()
        {
            throw new NotImplementedException();
        }

        public void StartVisuals()
        {
            throw new NotImplementedException();
        }
    }
}
