using System.Collections.Generic;
using UnityEngine;

namespace foursoulsauto.core.deck
{
    public class DeckWithSlots : DeckBehaviour
    {
        [SerializeField] private List<CardPile> slots;

        public List<CardPile> Slots => slots;

        private void Awake()
        {
            slots.ForEach(slot => slot.Emptied += OnSlotEmptied);
        }

        private void OnSlotEmptied()
        {
            // TODO: delay this (make it an effect)
            
        }
    }
}