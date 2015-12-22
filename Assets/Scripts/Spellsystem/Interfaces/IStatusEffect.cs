using UnityEngine;
using System.Collections;


namespace Spellsystem
{
    public interface IStatusEffect
    {
        /// <summary>
        /// This function handles the effect input / state change
        /// </summary>
        /// <param name="information">necessary data</param>
        void RecvEffect(SpellInformation information);
    }
}