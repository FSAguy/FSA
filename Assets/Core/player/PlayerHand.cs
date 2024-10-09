using System.Collections.Generic;
using UnityEngine;

namespace core
{
    //TODO: make cards visible only to client player
    //first uuhhhh make multiplayer work
    public class PlayerHand : PlayerCardContainer
    {
        [SerializeField] private BoxCollider zone;
        [SerializeField] private float prefDistance;

        public override ContainerType ConType => ContainerType.Hand;

        protected override void Add(Card card)
        {
            base.Add(card);
            card.ShowCard();
            card.FaceUp = true; 
            UpdateCardArrangement();
        }

        protected override void Add(List<Card> cards)
        {
            base.Add(cards);
            foreach (var card in cards)
            {
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