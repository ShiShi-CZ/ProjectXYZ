﻿using UnityEngine;
using System.Collections;
using Spellsystem;

namespace Player
{

    /// <summary>
    /// Handles all types of spells. Continuous, Charged, Burst. 
    /// </summary>
    public class SpellHandler : MonoBehaviour
    {

        #region Editor
        public int MaxChargeTime;
        public int MaxRamp;
        public int RampStep;
        public int RampInterval;
        #endregion

        float ChargeTime;
        bool charge = false;
        int Ramp;
        

        SpellLogic currentSpell;

        bool continuous;

        /// <summary>
        /// Gets called when a new Spell is generated
        /// </summary>
        /// <param name="sl">the generated spell</param>
        /// 

        void Awake()
        {
            SpellGenerator.Instance.SpellGenerated += newSpellGenerated;
        }

        void newSpellGenerated(SpellLogic sl)
        {
            currentSpell = sl;
            if (sl.SpellInformation.Spelltype == SpellType.Charged)
            {
                // Set IsCharging() 
                charge = true;
                StartCoroutine("Charge");
            }

            else if (sl.SpellInformation.Spelltype == SpellType.Continuous)
            {
                // Set Animation based on the Spellform like Spray. If the number of animations grows, we can add a Dictionary that contains Delegates, so it also has constant time.
                continuous = true;
                sl.gameObject.SetActive(true);
                StartCoroutine("Continuous");
            }
           
        }


        /// <summary>
        /// Starts the charging process
        /// </summary>
        /// <returns></returns>
        IEnumerator Charge()
        {
            while (ChargeTime < MaxChargeTime && charge)
            {
                yield return new WaitForSeconds(0.05f);
                ChargeTime += 0.05f;
            }


            currentSpell.SetDamage(ChargeTime, 1); // Ramp = 1;
            currentSpell.gameObject.SetActive(true); // Turn it on.

            //Reset ChargeTime
            ChargeTime = 0;
        }

        /// <summary>
        /// Starts the Continuous process; Damage gets handled here.
        /// </summary>
        /// <returns></returns>
        IEnumerator Continuous()
        {
            while (Ramp < MaxRamp && continuous)
            {
                yield return new WaitForSeconds(RampInterval);
                Ramp += RampStep;
                currentSpell.SetDamage(1, Ramp);
            }

            currentSpell.Kill();

            //Reset Ramp
            Ramp = 0;
        }

        /// <summary>
        /// Gets called when a button is released during the same frame.
        /// </summary>
        public void OnButtonUp()
        {
            charge = false;
            continuous = false;
        }

    }
}
