using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core
{
    public class CardPile : CardContainer
    { // TODO: card piles are either A - Draws, B - Discards, C - Monster Piles, D - Shops, E - Rooms
        // perhaps make separate classes???
        private readonly List<Card> _cards = new();
        private SpriteRenderer _defaultRender;

        private void Awake()
        {
            _defaultRender = GetComponent<SpriteRenderer>();
        }

        protected override void Add(Card card)
        {
            _cards.Insert(0, card);
            card.MoveTo(transform);
            Invoke(nameof(UpdateGraphic), Card.MOVETIME);
        }

        protected override void Add(List<Card> cards)
        {
            for (var i = 0; i < cards.Count; i++)
            {
                var time = Card.MOVETIME * Mathf.Pow((1f + i) / cards.Count, 2);
                var card = cards[i];
                _cards.Insert(0, card);
                card.MoveTo(transform, time);
            }
            Invoke(nameof(UpdateGraphic), Card.MOVETIME);
        }


        protected override void Remove(Card card)
        {
            _cards.Remove(card);
            UpdateGraphic();
        }

        protected override void Remove(List<Card> cards)
        {
            _cards.RemoveAll(cards.Contains);
            UpdateGraphic();
        }

        private void UpdateGraphic()
        {
            for (var i = 1; i < _cards.Count; i++)
                _cards[i].HideCard();
            
            if (_cards.Count > 0)
            {
                if (_defaultRender != null) _defaultRender.enabled = false;
                _cards[0].ShowCard();
            }
            else
            {
                _cards[0].HideCard();
                if (_defaultRender != null) _defaultRender.enabled = true;
            }
        }

        protected override bool CanAdd(Card card) => true;
        protected override bool CanAdd(List<Card> cards) => true;

        protected override bool CanRemove(Card card)
        {
            return _cards.Contains(card);
        }

        protected override bool CanRemove(List<Card> cards)
        {
            return cards.All(card => _cards.Contains(card));
        }
    }
}