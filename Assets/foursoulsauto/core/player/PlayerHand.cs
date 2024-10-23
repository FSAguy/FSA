using System.Collections.Generic;
using foursoulsauto.ui;
using UnityEngine;

namespace foursoulsauto.core.player
{
    public class PlayerHand : CardContainer
    {
        // TODO: make the cards squish together when there are too many of them
        // TODO: fix location error from changing the pivot of the cards to be top left
        [SerializeField] private BoxCollider zone;
        [SerializeField] private float prefDistance;

        protected override void Add(Card card)
        {
            base.Add(card);
            // TODO: should probably use "enter play" or something
            card.ShowCard();
            //TODO: make cards visible only to client player
            //first uuhhhh make multiplayer work
            card.FaceUp = true; 
            UpdateCardArrangement();
        }

        protected override void Add(List<Card> cards)
        {
            base.Add(cards);
            foreach (var card in cards)
            {
                // TODO: should probably use "enter play" or something
                card.ShowCard();
                card.FaceUp = true;
            }
            UpdateCardArrangement();
        }

        protected override void Remove(Card card)
        {
            base.Remove(card);
            UpdateCardArrangement();
        }

        protected override void Remove(List<Card> cards)
        {
            base.Remove(cards);
            UpdateCardArrangement();
        }
        private void UpdateCardArrangement()
        {
            var count = Cards.Count;
            var indexSum = count * (count - 1)/ 2f;
            var avg = Vector3.right * (prefDistance * indexSum) / count;
            for (var i = 0; i < count; i++)
            {
                var pos = Vector3.right * (i * prefDistance) - avg;
                Cards[i].MoveTo(pos, Quaternion.identity, local: true);
            }
        }
    }
}