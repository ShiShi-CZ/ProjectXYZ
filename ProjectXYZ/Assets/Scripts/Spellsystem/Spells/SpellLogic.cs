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
    /// 
    /// TODO: There is currently redundancy because you have to create prefabs like DeathBeam or FireDeathbeam. However this should be avoided.
    ///       Idea: VFXHandler plays a Spellsystem (located in a pool - which could also be global - of possible Systems) based on the two least important elements. (Like Fire and Death)
    /// 
    /// </summary>
    public abstract class SpellLogic : MonoBehaviour
    {

        protected VFXHandler vfx;
        public delegate void SpellExpiredHandler(object sender, EventArgs e);
        public event SpellExpiredHandler SpellExpired;
        
        protected Damage Damage;

        /// <summary>
        /// Actual Lifetime of a spell. Controls how long the spell can be seen on screen.
        /// </summary>
        public int LifetimeInSeconds;

        /// <summary>
        /// Internal so its not visible for the editor
        /// </summary>
        internal SpellType Spelltype;
        internal SpellForm Spellform;


        /// <summary>
        /// This should be the bone/socket of the staff for example; Set it in the editor.
        /// </summary>
        public Transform SpellPosition;

        protected virtual void Awake()
        {
            vfx = GetComponent<VFXHandler>();
            Debug.Assert(vfx != null, "WARNING: VFXHandler is not part of this Prefab, but it needs to.");
            StartCoroutine("CountToDeath");
            transform.rotation = SpellPosition.rotation;
            transform.position = SpellPosition.position;
        }

        public void SetDamage(float charge, int ramp)
        {
            Damage.ChargeTime = charge;
            Damage.Ramp = ramp;
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
                damageReceiver.RecvDamage(Damage);
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
            Damage.Elements = elements;
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