using UnityEngine;
using Spellsystem;

namespace Player
{
    public class ElementInput : MonoBehaviour
    {

        ElementStack stack;
        SpellHandler handler;
        SpellLogic currentSpell;

        void Awake()
        {
            stack = GetComponent<ElementStack>();
            handler = GetComponent<SpellHandler>();
            Debug.Assert(stack != null, "This script depends on ElementStack which is not a component.");
            Debug.Assert(handler != null, "This script depends on SpellGenerator which is not a component.");
        }

        // Unoptimized.
        void Update()
        {

            if (Input.anyKey)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    stack.Push(new Element(ElementType.Q, ElementType.A, 1, default(Color)));
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    stack.Push(new Element(ElementType.W, ElementType.S, 2, default(Color)));
                }
                if (Input.GetKeyDown(KeyCode.E))
                {
                    stack.Push(new Element(ElementType.E, ElementType.D, 3, default(Color)));
                }
                if (Input.GetKeyDown(KeyCode.R))
                {
                    stack.Push(new Element(ElementType.R, ElementType.F, 4, default(Color)));
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    stack.Push(new Element(ElementType.A, ElementType.Q, 5, default(Color)));
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    stack.Push(new Element(ElementType.S, ElementType.W, 6, default(Color)));
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    stack.Push(new Element(ElementType.D, ElementType.E, 7, default(Color)));
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    stack.Push(new Element(ElementType.F, ElementType.R, 8, default(Color)));
                }

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    stack.PopAll();
                }
            }

            if (stack.AnyElementOnStack && currentSpell == null)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    currentSpell = SpellGenerator.Instance.GenerateSpell(stack.GetStack(), false);
                    stack.PopAll();
                }

                else if (Input.GetMouseButtonDown(2))
                {
                    currentSpell = SpellGenerator.Instance.GenerateSpell(stack.GetStack(), true);
                    stack.PopAll();
                }
            }

            if(Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
            {
                handler.OnButtonUp();
                currentSpell = null;
            }
        }
    }
}