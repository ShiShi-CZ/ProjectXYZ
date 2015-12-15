using UnityEngine;
using System;

namespace Spellsystem
{
    public enum ElementType
    {
        Q, W, E, R, A, S, D, F, Default
    }


    /// <summary>
    /// This is the class that stores data for the different Elements.
    /// NOTE: I might go back to Sapling's IElement because it saves much more time when creating Elements;
    /// </summary>
    public class Element : IComparable
    {

        public Element(ElementType name, ElementType opposingElementName, short priority, Color buttonColor)
        {
            OpposingElementName = opposingElementName;
            Priority = priority;

            Name = name;
        }

        public Element()
        {

            OpposingElementName = ElementType.Default;
            Name = ElementType.Default;
            Priority = 0;
        }

        // Every Element has a specific, suggested damage value. 
        public float BaseDamage { get; private set; }
        public ElementType OpposingElementName { get; private set; }
        public short Priority { get; private set; }
        public ElementType Name { get; private set; }

        public int CompareTo(object obj)
        {
            Debug.Assert(obj is Element, "CompareTo: Object was not a type of 'Element'");
            Element element = obj as Element;
            return Priority.CompareTo(element.Priority);
        }
    }
}