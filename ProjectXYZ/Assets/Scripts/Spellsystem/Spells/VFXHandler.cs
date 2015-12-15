using System;
using UnityEngine;

namespace Spellsystem
{
    /// <summary>
    /// This interface handles the Visual like Particle Effects e.g. based on the Spellform. 
    /// </summary>
    public interface VFXHandler
    {
        void DeathVisuals();

        void StartVisuals();
    }
}