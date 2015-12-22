using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

namespace Spellsystem
{

    public enum SpellType { Charged, Continuous, Burst }
    public enum SpellForm
    {
        Spray,
        Rock
    }

    /// <summary>
    /// Base class for every Spell in game. 
    /// To create a new Spell inherit from this. This should only contains the logic. Movement etc. should be handled in component class. 
    /// </summary>
    /// 

    
    public abstract class SpellLogic : MonoBehaviour
    {

        public delegate void SpellExpiredHandler(object sender, EventArgs e);
        public event SpellExpiredHandler SpellExpired;

        public SpellInformation SpellInformation;
       
        /// <summary>
        /// Actual Lifetime of a spell. Controls how long the spell can be seen on screen. Editor
        /// </summary>
        public int LifetimeInSeconds;

        /// <summary>
        /// This should be the bone/socket of the staff for example.
        /// </summary>
        public Transform StaffTransform { get; set; }

        protected virtual void Awake()
        {
            StartCoroutine("CountToDeath");
            transform.rotation = StaffTransform.rotation;
            transform.position = StaffTransform.position;
        }

        public void SetDamage(float charge, int ramp)
        {
            SpellInformation.ChargeTime = charge;
            SpellInformation.Ramp = ramp;
        }

        /// <summary>
        /// Use this to apply the damage. To apply it, test if the gobject implements IDamageAble; It true, call RecvDmg by passing the Dmg specified above;
        /// The object "knows" how to handle that damage. E.g. The player has warded !ESS and you want to apply Pure death damage, the spell doesn't need to know that the player has warded !ESS.
        /// But the player will internally handle this damage input and turn it down to zero.
        /// You guys know what i mean :D
        /// 
        /// Use SendMessage OR GetComponents<typeof(IDamageAble)>() on gobject to get the interface. 
        /// </summary>
        /// <param name="gobject">The gameobject that should recv the Dmg.</param>

        public virtual void ApplyDamage(GameObject gobject)
        {
            IDamageable damageReceiver = gobject.GetComponent(typeof(IDamageable)) as IDamageable;
            if (damageReceiver != null)
            {
                damageReceiver.RecvDamage(SpellInformation);
            }
        }

        protected IEnumerator CountToDeath()
        {
            yield return new WaitForSeconds(LifetimeInSeconds);
            Kill();
        }

        protected void RaiseSpellExpired()
        {
            if (SpellExpired != null)
            {
                SpellExpired(this, new EventArgs());
            }
        }

        public void SetElements(List<Element> elements)
        {
            SpellInformation.Elements = elements;
        }

        /// <summary>
        /// Use this to apply an specific effect 
        /// Create your own Interface (e.g. IFlamable) and check if the GameObject implements it. If true, just call your methods defined in your Interface. 
        /// </summary>
        /// <param name="gobject"> The object that should recv the effect</param>
        public abstract void ApplyEffect(GameObject gobject);

        /// <summary>
        /// Implement this to handle everything before destroying this. E.g. make something fade out etc.
        /// </summary>
        public abstract void Kill();
    }
}