using UnityEngine;
using System.Collections.Generic;

namespace Spellsystem
{
    /// <summary>
    /// This struct holds information to pass when a hit occurs.Access it where you want;
    /// </summary>
    public struct SpellInformation
    {
        public List<Element> Elements;
        public SpellForm Spellform;
        public SpellType Spelltype;
        public float Ramp;
        public float ChargeTime;
    }

}