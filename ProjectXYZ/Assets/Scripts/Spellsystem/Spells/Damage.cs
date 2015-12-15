using UnityEngine;
using System.Collections.Generic;

namespace Spellsystem
{
    /// <summary>
    /// This struct holds information to pass when a hit occurs.Access it where you want;
    /// </summary>
    public struct Damage
    {
        public List<Element> Elements;
        public SpellForm Spellform;
        public float Ramp;
        public float ChargeTime;
    }

}