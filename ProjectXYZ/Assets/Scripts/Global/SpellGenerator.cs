using UnityEngine;
using System.Collections.Generic;
using System.Text;
using Spellsystem;
using System;

namespace Player
{
    /// <summary>
    /// There are 3 different types of spells.
    /// 1. Burst, only one Element in the Q
    /// 2. Continous, two Elements in the Q
    /// 3. Charged, three elements in the Q
    /// 
    /// 
    /// The dmg is determined by the two least important elements.
    /// 
    /// So this is an abstract approach to create specific spells
    /// 
    /// 
    /// NOTE: I admit the dictionary approach is hacky and pretty hardcoded, but also very fast. We will have a lot of spells and a hashmap lookup has constant lookup time.
    /// This saves much time. If we decide to modify it, then we should do this later. "Make It Work, Make It Right, Make It Fast" Kent Beck
    /// 
    /// </summary>

    public class SpellGenerator : MonoBehaviour
    {

        /// <summary>
        /// Values and keys needs to be assigned before the game starts. A Key must have the following syntax: 
        /// xyz, with x,y,z being either the capital letter of the Elements (e.g. Q,S,D..) or epsilon; (Forward Cast)
        /// OR
        /// !xyz, with x,y,z being either capital letters of the Elements or epsilon; (Self Cast)
        /// 
        /// Semantics: xyz must be ordered according to the priority of the element starting with the highest priority. Otherwise the right spell isn't found. 
        /// IMPPORTANT CHANGE: The keys are associated with SPELLFORMS e.g. Spray, Beam etc.
        ///                    So don't create redundant classes like FireSpray, ColdSpray etc.; Instead make Particle Effects (Visual) and specify the concrete form (DeathBeam) in the inspector.  
        ///
        /// This allows the graphic team to create artwork and stuff like that without consulting us. 
        /// 
        /// TODO: Put this into another GameObject ---> global pool => less MB. 
        /// </summary>
        public List<string> ElementCombination;

        public List<SpellLogic> Spells;
        Dictionary<string, SpellLogic> spellbook = new Dictionary<string, SpellLogic>();

        public delegate void SpellGeneratedHandler(SpellLogic sl);
        public event SpellGeneratedHandler SpellGenerated;

        public static SpellGenerator Instance
        { get; private set; }


        void Awake()
        {

            // From http://clearcutgames.net/home/?p=437;
            //-----
            //It seems weird to me because we destroy our gameObject and then we attach a class that depends on that to the instance?
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }

            Instance = this;
            //------



            // This is only called once, so it has no performance impact
            Debug.Assert(Spells.TrueForAll((SpellLogic sl) => sl != null),"CRITICAL ERROR: Not every spellslot was set in the Spellgenerator skript.");
            Debug.Assert(ElementCombination.Count == Spells.Count, "CRITICAL ERROR: The amount of element combinations has to be the same as the amount of spells in the Spellgenerator skript.");

            for(int i = 0; i<ElementCombination.Count;i++)
            {
                Spells[i].gameObject.SetActive(false);
                spellbook.Add(ElementCombination[i], Spells[i]);
            }

            ElementCombination = null; // Get rid of these things because we don't need them anymore. This should be done during a loading screen (like in MWW)
            Spells = null; // I know its just a reference. :P

        }


        /// <summary>
        /// Call this if you want to create a spell.
        /// </summary>
        /// <param name="elements">The element stack</param>
        /// <param name="self">Selfcast?</param>
        /// <returns>The generated spell containing all necessary information</returns>
        /// Compexity: O(1) 
        public SpellLogic GenerateSpell(List<Element> elements, bool selfcast)
        {
            elements.Sort();
            int n = elements.Count;
            SpellLogic sl = default(SpellLogic);

            StringBuilder name;
            name = initializeStringBuilder(selfcast, n);
            getStringRepr(elements, name);

            Debug.Assert(spellbook.ContainsKey(name.ToString()), "CRITICAL ERROR: Given Element Combination "+name.ToString() +" is not a valid key.");

            sl = Instantiate(spellbook[name.ToString()]);
            sl.SetElements(elements); // we might need to copy that when shared amongst players.
            RaiseNewSpellGenerated(sl);
            return sl;

        }

        private void RaiseNewSpellGenerated(SpellLogic sl)
        {
            if(SpellGenerated != null)
            {
                SpellGenerated(sl);
            }
        }

        private void getStringRepr(List<Element> elements, StringBuilder name)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                name.Append(elements[i].ToString());
            }
        }

        private StringBuilder initializeStringBuilder(bool selfcast, int n)
        {
            StringBuilder name;
            if (selfcast)
            {
                name = new StringBuilder(n + 1);
                name.Append("!");
            }

            else
            {
                name = new StringBuilder(n);
            }
            return name;
        }
        #region Deprecated
        /*
        public SpellLogic GenerateSpellForward(List<Element> elements)
        {
            elements.Sort();
            int n = elements.Count;

            StringBuilder name = new StringBuilder(n);
            for(int i=0; i<elements.Count; i++) 
            {
                name.Append(elements[i].Name.ToString());
            }

            SpellLogic sl = (SpellLogic) Instantiate(spellbook[name.ToString()], Vector3.zero, default(Quaternion));

            SendMessage("NewSpellCasted",sl); // Benchmarking shows that calling this every second doesn't hurt performance. We can replace that later. 

            //Alternate sl so it fits the situation etc. Change the signature of the function as you wish, so it provides necessary arguments.
            //But from now on sl should do all the logic, e.g. where to fly or putting down mines etc. 
            //SpellGenerator shouldn't do this. However some initialization properties can be set here.

            return sl;
        }

        public SpellLogic GenerateSpellSelf(List<Element> elements)
        {

            elements.Sort();
            string name = "!";
            for (int i = 0; i < elements.Count; i++)
            {
                name += elements[i].Name.ToString();
            }

            SpellLogic sl = (SpellLogic)Instantiate(spellbook[name], Vector3.zero, default(Quaternion));
            SendMessage("NewSpellCasted", sl);
            //Same as above

            return sl;
        }
        */
        #endregion
    }
}