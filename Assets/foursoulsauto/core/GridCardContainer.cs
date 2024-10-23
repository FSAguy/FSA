using System.Collections.Generic;
using foursoulsauto.ui;
using UnityEngine;

namespace foursoulsauto.core.player
{
    // TODO: figure out what to do when there are too many cards
    public class GridCardContainer : CardContainer
    {
        [SerializeField] private BoxCollider zone;
        [SerializeField] private int rows;
        [SerializeField] private int cols;

        protected override void Add(Card card)
        {
            base.Add(card);
            // should probably use 
            card.ShowCard();
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

        private void UpdateCardArrangement()
        {
            var bounds = zone.bounds;
            var topLeft = Vector3.left * bounds.extents.x + Vector3.up * bounds.extents.y;
            var colLength = bounds.size.x / cols;
            var rowLength = bounds.size.y / rows;
            
            for (var i = 0; i < Cards.Count; i++)
            {
                var card = Cards[i];
                
                var col = i % cols;
                var row = i / cols;
                        
                var pos = topLeft + Vector3.right * (colLength * col) + Vector3.down * (rowLength * row);

                // TODO: Rotations absolutely do not work when z != 0 or 180
                card.MoveTo(pos, zone.transform.rotation, local:true);
            }
        }
    }
}