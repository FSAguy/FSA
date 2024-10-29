using System.Collections.Generic;
using foursoulsauto.core.effectlib;
using UnityEngine;

namespace foursoulsauto.core.deck
{
    public class DeckWithSlots : DeckBehaviour
    {
        [SerializeField] private List<CardPile> slots;

        public List<CardPile> Slots => slots;

        public override void Setup(List<Card> draw, List<Card> discard = null)
        {
            base.Setup(draw, discard);
            Slots.ForEach(slot => DrawInto(slot, 1));
        }

        private void Awake()
        {
            slots.ForEach(slot => slot.Emptied += OnSlotEmptied);
        }

        private void OnSlotEmptied(CardPile slot)
        {
            Board.Instance.AddEffect(new RefillSlotEffect(this, slot));
        }
    }
}