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

        protected override void AfterCardsAdded(List<Card> addedCards)
        {
            foreach (var card in addedCards)
            {
                // TODO: should probably use "enter play" or something
                card.ShowCard();
                card.FaceUp = true;
            }
            UpdateCardArrangement();
        }

        protected override void AfterCardsRemoved(List<Card> removedCards)
        {
            UpdateCardArrangement();
        }

        private void UpdateCardArrangement()
        {
            if (Cards.Count * prefDistance < zone.size.x) SpaciousArrangement();
            else TightArrangement();
        }

        private void TightArrangement()
        {
            var count = Cards.Count;
            var size = zone.size;
            var topLeftFront = Vector3.Scale(size, new Vector3(-1, 1, 1) / 2);
            var space = size.x / count * Vector3.right + size.z / count * Vector3.back;

            for (var i = 0; i < count; i++)
            {
                var pos = topLeftFront + space * i;
                Cards[i].MoveTo(pos, Quaternion.identity, local:true);
            }
        }

        private void SpaciousArrangement()
        {
            var count = Cards.Count;
            var indexSum = count * (count - 1)/ 2f;
            var avg = Vector3.right * (prefDistance * indexSum) / count;
            var topCenter = zone.size.y / 2 * Vector3.up;
            for (var i = 0; i < count; i++)
            {
                var pos = topCenter + Vector3.right * (i * prefDistance) - avg;
                Cards[i].MoveTo(pos, Quaternion.identity, local: true);
            }
        }
    }
}