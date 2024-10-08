using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public class CardPile : CardContainer
    { // TODO: card piles are either A - Draws, B - Discards, C - Monster Piles, D - Shops, E - Rooms
        // perhaps make separate classes???
        [SerializeField] private bool faceUp = false;
        private SpriteRenderer _defaultRender;
        

        private void Awake()
        {
            _defaultRender = GetComponent<SpriteRenderer>();
        }

        public override ContainerType ConType => ContainerType.Deck; // TODO: make cardpile abstract (See prev comment)

        protected override void Add(Card card)
        {
            base.Add(card);
            card.ShowCard();
            card.FaceUp = faceUp;
            card.MoveTo(transform);
            Invoke(nameof(UpdateTopRender), CardAnimate.MoveTime);
        }

        protected override void Add(List<Card> cards)
        {
            base.Add(cards);
            for (var i = 0; i < cards.Count; i++)
            {
                 var time = CardAnimate.MoveTime * Mathf.Pow((1f + i) / cards.Count, 2);
                 cards[i].ShowCard();
                 cards[i].MoveTo(transform, time);
                 cards[i].FaceUp = faceUp;
            }
            Invoke(nameof(UpdateTopRender), CardAnimate.MoveTime);
        }


        protected override void Remove(Card card)
        {
            base.Remove(card);
            UpdateTopRender();
        }

        protected override void Remove(List<Card> cards)
        {
            base.Remove(cards);
            UpdateTopRender();
        }

        private void UpdateTopRender()
        {
            for (var i = 0; i < Cards.Count - 1; i++)
                Cards[i].HideCard();
            
            if (Cards.Count > 0)
            {
                if (_defaultRender != null) _defaultRender.enabled = false;
                Cards.Last().ShowCard();
            }
            else if (_defaultRender != null) _defaultRender.enabled = true;
        }
    }
}