﻿using System.Collections.Generic;
using UnityEngine;

namespace foursoulsauto.core.deck
{
    public class DeckBehaviour : MonoBehaviour
    {
        // TODO: shuffle function
        // TODO: reshuffle discard after draw emptied
        [SerializeField] protected Deck deckType;
        [SerializeField] protected CardPile drawPile;
        [SerializeField] protected CardPile discardPile;

        public Deck DeckType => deckType;

        public Card TopDraw => drawPile.Top;
        public Card TopDiscard => discardPile.Top;

        public List<Card> DrawCards => drawPile.Cards;
        public List<Card> DiscardCards => discardPile.Cards;
        
        public virtual void Setup(List<Card> draw, List<Card> discard = null, bool shuffle = false)
        {
            drawPile.MoveInto(draw);
            discardPile.MoveInto(discard);
            if (shuffle) drawPile.Shuffle();
        }

        public void DrawInto(CardContainer container, int amount)
        {
            var draw = drawPile.Cards;
            var index = drawPile.Cards.Count - amount;

            container.MoveInto(draw.GetRange(index, amount));
        }

        public void DiscardInto(Card card)
        {
            discardPile.MoveInto(card);
        }

        public void DiscardInto(List<Card> cards)
        {
            discardPile.MoveInto(cards);
        }
    }
}