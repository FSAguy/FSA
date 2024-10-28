using System.Collections.Generic;
using System.Linq;
using foursoulsauto.ui;
using UnityEngine;

namespace foursoulsauto.core
{
    public class CardPile : CardContainer
    { // TODO: card piles are either A - Draws, B - Discards, C - Monster Piles, D - Shops, E - Rooms
        // perhaps make separate classes???
        [SerializeField] private bool faceUp;

        public Card Top => Cards.LastOrDefault();

        protected override void Add(Card card)
        {
            base.Add(card);
            card.ShowCard();
            card.FaceUp = faceUp;
            card.MoveTo(transform, style: CardAnimate.Style.Fall);
            CancelInvoke();
            Invoke(nameof(UpdateTopRender), CardAnimate.AnimTime);
        }

        protected override void Add(List<Card> cards)
        {
            base.Add(cards);
            for (var i = 0; i < cards.Count; i++)
            {
                 var time = CardAnimate.AnimTime * Mathf.Pow((i + 1f) / Cards.Count, 2);
                 cards[i].ShowCard();
                 cards[i].MoveTo(transform, time, CardAnimate.Style.Fall);
                 cards[i].FaceUp = faceUp;
            }
            CancelInvoke();
            Invoke(nameof(UpdateTopRender), CardAnimate.AnimTime);
        }


        protected override void Remove(Card card)
        {
            base.Remove(card);
            if (!IsInvoking(nameof(UpdateTopRender)))
                UpdateTopRender();
        }

        protected override void Remove(List<Card> cards)
        {
            base.Remove(cards);
            if (!IsInvoking(nameof(UpdateTopRender)))
                UpdateTopRender();
        }

        private void UpdateTopRender()
        {
            for (var i = 0; i < Cards.Count - 1; i++)
                Cards[i].HideCard();
            if (Top is not null) Top.ShowCard();
        }
    }
}