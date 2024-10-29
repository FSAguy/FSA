using System;
using System.Collections.Generic;
using System.Linq;
using foursoulsauto.ui;
using UnityEngine;
using Random = UnityEngine.Random;

namespace foursoulsauto.core
{
    public class CardPile : CardContainer
    { // TODO: card piles are either A - Draws, B - Discards, C - Monster Piles, D - Shops, E - Rooms
        // perhaps make separate classes???
        [SerializeField] private bool faceUp;

        public event Action<CardPile> Emptied;

        public Card Top => Cards.LastOrDefault();

        protected override void AfterCardsAdded(List<Card> addedCards)
        {
            for (var i = 0; i < addedCards.Count; i++)
            {
                var time = CardAnimate.AnimTime * Mathf.Pow((i + 1f) / addedCards.Count, 2);
                addedCards[i].ShowCard();
                addedCards[i].MoveTo(transform, time, CardAnimate.Style.Fall);
                addedCards[i].FaceUp = faceUp;
            }
            CancelInvoke();
            Invoke(nameof(UpdateTopRender), CardAnimate.AnimTime);
        }

        protected override void AfterCardsRemoved(List<Card> removedCards)
        {
            if (!IsInvoking(nameof(UpdateTopRender)))
                UpdateTopRender();
            if (Cards.Count == 0) Emptied?.Invoke(this);
        }

        private void UpdateTopRender()
        {
            for (var i = 0; i < Cards.Count - 1; i++)
                Cards[i].HideCard();
            if (Top is not null) Top.ShowCard();
        }

        public void Shuffle()
        {
            // TODO: shuffle animation
            for (var i = Cards.Count - 1; i > 0; i--)
            {
                var rand = Random.Range(0, i + 1);
                (Cards[rand], Cards[i]) = (Cards[i], Cards[rand]);
            }
        }
    }
}