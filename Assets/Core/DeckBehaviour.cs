using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class DeckBehaviour : MonoBehaviour
    {
        [SerializeField] private CardPile _drawPile;
        [SerializeField] private CardPile _discardPile;


        public void Setup(List<Card> draw, List<Card> discard = null)
        {
            _drawPile.MoveInto(draw);
            _discardPile.MoveInto(discard);
        }
    }
}