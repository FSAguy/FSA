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
        public List<Card> Cards { get; } = new();

        protected virtual void Awake()
        {
            Owner = GetComponentInParent<Player>();
        }

        // TODO: maybe make CardContainer responsible for the visibility of the card?
        public Player Owner { get; private set; }
        
        protected abstract void AfterCardsAdded(List<Card> addedCards);
        protected abstract void AfterCardsRemoved(List<Card> removedCards);

        protected virtual void AfterCardsInserted(List<Card> insertedCards, int index)
            => AfterCardsAdded(insertedCards);

        private void Add(List<Card> cards)
        {
            foreach (var card in cards)
            {
                Cards.Add(card);
                card.Container = this;
                card.transform.SetParent(transform);
            }
            
            AfterCardsAdded(cards);
        }

        private void Insert(List<Card> cards, int index)
        {
            Cards.InsertRange(index, cards);
            foreach (var card in cards)
            {
                card.Container = this;
                card.transform.SetParent(transform);
            }
            
            AfterCardsInserted(cards, index);
        }
        
        private void Remove(List<Card> cards)
        {
            foreach (var card in cards) Cards.Remove(card); 
            AfterCardsRemoved(cards);
        }
        
        public void MoveInto(Card card)
        {
            MoveInto(new List<Card> { card });
        }
        
        public void MoveInto(List<Card> cards)
        {
            if (cards == null) return;
            
            RemoveAllFromOwners(cards);
            
            Add(cards);
        }

        public void InsertInto(Card card, int index)
        {
            InsertInto(new List<Card> {card}, index);
        }

        public void InsertInto(List<Card> cards, int index)
        {
            if (cards == null) return;
            
            RemoveAllFromOwners(cards);
            
            Insert(cards, index);
        }

        private static void RemoveAllFromOwners(IEnumerable<Card> cardsToRemove)
        {
            // first divide cards up by their owners, then remove them simultaneously for performance
            var ownerToCards = new Dictionary<CardContainer, List<Card>>();
            foreach (var card in cardsToRemove.Where(card => card.Container != null))
            {
                var container = card.Container;
                if (!ownerToCards.ContainsKey(container))
                    ownerToCards.Add(container, new List<Card>());
                ownerToCards[container].Add(card);
            }
            
            foreach (var (container, cardList) in ownerToCards)
                container.Remove(cardList);
        }
    }
}