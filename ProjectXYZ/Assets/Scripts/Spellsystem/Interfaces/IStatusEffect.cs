using UnityEngine;
using System.Collections;


namespace Spellsystem
{
    public interface IStatusEffect
    {
        /// <summary>
        /// Receives effect based on the input
        /// </summary>
        /// <param name="damage">The incoming damage</param>
        /// <param name="type">The damage type, for example Continuous</param>
        void RecvEffect(Damage damage, SpellType type);
    }
}