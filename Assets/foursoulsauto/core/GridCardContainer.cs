﻿using System.Collections.Generic;
using foursoulsauto.ui;
using UnityEngine;

namespace foursoulsauto.core
{
    // TODO: figure out what to do when there are too many cards
    public class GridCardContainer : CardContainer
    {
        [SerializeField] private BoxCollider zone;
        [SerializeField] private int rows;
        [SerializeField] private int cols;

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
            var size = zone.size;
            var topLeft = (Vector3.left * size.x + Vector3.up * size.y) / 2;
            var colLength = size.x / cols;
            var rowLength = size.y / rows;
            
            for (var i = 0; i < Cards.Count; i++)
            {
                var card = Cards[i];
                
                var col = i % cols;
                var row = i / cols;
                        
                var pos = topLeft + Vector3.right * (colLength * col) + Vector3.down * (rowLength * row);

                card.MoveTo(pos, Quaternion.identity, local:true);
            }
        }
    }
}