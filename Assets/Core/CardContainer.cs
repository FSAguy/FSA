using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    // TODO: this entire thing must be changed.
    // We need a way to switch between physical cards and currently inaccessible cards (like, in a deck)
    public abstract class CardContainer : MonoBehaviour
    {
        // TODO: maybe make CardContainer responsible for the visibility of the card?
        protected abstract void Add(Card card);
        protected abstract void Add(List<Card> cards);
        protected abstract void Remove(Card card);
        protected abstract void Remove(List<Card> cards);
        protected abstract bool CanAdd(Card card);
        protected abstract bool CanAdd(List<Card> cards);
        protected abstract bool CanRemove(Card card);
        protected abstract bool CanRemove(List<Card> cards);

        public bool MoveInto(Card card)
        {
            var other = card.Container;
            if (!CanAdd(card) || (other != null && !other.CanRemove(card)))
            {
                Debug.LogError(
                    $"Attempted RemoveAndAdd illegally: {card} from {other} to {this}."
                ); 
                return false;
            } 
            
            if (other != null) other.Remove(card);
            
            Add(card);
            card.Container = this;
            return true;
        }
        
        // returns true if list is null
        public bool MoveInto(List<Card> cards)
        {
            // TODO: this check doesnt actually check "mass removal validity"
            // maybe just remove CanRemove for lists instead of overthinking an algorithm?
            if (cards == null) return true;
            if (!cards.All(card => card.Container == null || card.Container.CanRemove(card))) return false;
            if (!CanAdd(cards)) return false;
            foreach (var card in cards.Where(card => card.Container != null))
            {
                card.Container.Remove(card);
            }
            Add(cards);
            return true;
        }
    }
}