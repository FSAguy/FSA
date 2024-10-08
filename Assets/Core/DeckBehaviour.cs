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

        public void DrawInto(CardContainer container, int amount)
        {
            var draw = _drawPile.Cards;
            var index = _drawPile.Cards.Count - amount;
            Debug.Log(index);

            container.MoveInto(draw.GetRange(index, amount));
        }

        public void DiscardInto(Card card)
        {
            _discardPile.MoveInto(card);
        }
    }
}