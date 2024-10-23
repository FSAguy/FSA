using System;
using System.Collections.Generic;
using System.Linq;
using foursoulsauto.core.player;
using UnityEngine;

namespace foursoulsauto.core
{
    // TODO: review this to see if it fulfills the needs of containing in play, not in play, and hidden (like in a deck) cards
    public abstract class CardContainer : MonoBehaviour
    {
        private Player _playerOwner; // will be null if controlled by board
        // TODO: maybe make CardContainer responsible for the visibility of the card?
        public List<Card> Cards { get; } = new();

        protected virtual void Awake()
        {
            _playerOwner = GetComponentInParent<Player>();
        }

        public Player Owner => _playerOwner;

        protected virtual void Add(Card card)
        {
            Cards.Add(card);
            card.Container = this;
            card.transform.SetParent(transform);
        }

        protected virtual void Add(List<Card> cards)
        {
            foreach (var card in cards) Add(card);
        }
        protected virtual void Remove(Card card) => Cards.Remove(card);

        protected virtual void Remove(List<Card> cards)
        {
            foreach (var card in cards) Cards.Remove(card); 
        }
        protected virtual bool CanAdd(Card card) => true;
        protected virtual bool CanAdd(List<Card> cards) => true;
        protected virtual bool CanRemove(Card card) => Cards.Contains(card);

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
            if (cards == null) return true;
            if (!cards.All(card => card.Container == null || card.Container.CanRemove(card))) return false;
            if (!CanAdd(cards)) return false;
            foreach (var card in cards.Where(card => card.Container != null))
                card.Container.Remove(card);
            Add(cards);
            return true;
        }
    }
}