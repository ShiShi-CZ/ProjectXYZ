using UnityEngine;
using System.Collections.Generic;
using Spellsystem;

namespace Player
{
    public class PushedEventArgs
    {
        public PushedEventArgs(Element pushedElement)
        {
            PushedElement = pushedElement;
        }

        public Element PushedElement { get; private set; }
    }

    public class RemovedEventArgs
    {
        public RemovedEventArgs(Element removedElement)
        {
            RemovedElement = removedElement;
        }

        public Element RemovedElement { get; private set; }
    }


    public class ElementStack : MonoBehaviour
    {



        public delegate void ElementPushedHandler(object sender, PushedEventArgs e);
        public event ElementPushedHandler ElementPushed;

        public delegate void ElementRemovedHandler(object sender, RemovedEventArgs e);
        public event ElementRemovedHandler ElementRemoved;



        List<Element> elements = new List<Element>(3);
        /// <summary>
        /// Pushes the element on the stack. If an opposing element exists, remove this element;
        /// TODO: Optimize it by using an Array (no resizing etc.) 
        /// </summary>
        /// <param name="element">Element that should be pushed back</param>
        public void Push(Element element)
        {


            Element opposingElement = elements.Find((Element otherElement) =>
            {
                return element.OpposingElementName == otherElement.ElementType; //Find opposing Element
        });


            if (opposingElement != null)
            {
                elements.Remove(opposingElement); // Remove that element, if not null
                RaiseElementRemoved(opposingElement);
                return;
            }

            // If less than 3 elements are in the stack, push that element back.
            if (elements.Count < 3)
            {
                elements.Add(element);
                RaiseOnElementPushed(element);
            }
        }


        public List<Element> GetStack()
        {
            return elements;
        }

        public void PopAll()
        {
            elements.Clear();
        }

        /// <summary>
        /// Is any Element currently stacked?
        /// </summary>
        public bool AnyElementOnStack { get { return elements.Count != 0; } }


        #region structure functions 
        private void RaiseElementRemoved(Element opposingElement)
        {
            if (ElementRemoved != null)
            {
                ElementRemoved.Invoke(this, new RemovedEventArgs(opposingElement));

            }
        }

        private void RaiseOnElementPushed(Element element)
        {
            if (ElementPushed != null)
            {
                ElementPushed(this, new PushedEventArgs(element));
            }
        }
        #endregion

    }
}