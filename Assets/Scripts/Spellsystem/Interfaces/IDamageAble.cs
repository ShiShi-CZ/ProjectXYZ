using UnityEngine;
using System.Collections.Generic;

namespace Spellsystem
{

    public interface IDamageable
    {
        /// <summary>
        /// This function handles the damage input
        /// </summary>
        /// <param name="information">necessary data</param>
        void RecvDamage(SpellInformation information);
    }
}