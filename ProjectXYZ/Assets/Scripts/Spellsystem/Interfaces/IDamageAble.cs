using UnityEngine;
using System.Collections.Generic;

namespace Spellsystem
{

    public interface IDamageable
    {
        /// <summary>
        /// Receive damage based on the input. 
        /// </summary>
        /// <param name="damage">The incoming damage</param>
        void RecvDamage(Damage damage);
    }
}