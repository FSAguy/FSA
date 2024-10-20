using System;
using System.Collections.Generic;

namespace foursoulsauto.core
{
    // way for a CardAction to request input from a player
    // TODO: other types of input
    public enum InputType {None, SingleCardTarget}
    public class EffectInput
    {
        public Card CardInput;
        private readonly Func<Card, bool> _cardPredicate;
        
        public InputType InpType { get; }

        public EffectInput()
        {
            InpType = InputType.None;
        }

        public EffectInput(Func<Card, bool> cardPredicate)
        {
            InpType = InputType.SingleCardTarget;
            _cardPredicate = cardPredicate;
        }

        public bool IsCardEligible(Card card)
        {
            return _cardPredicate(card);
        }

        public bool SomeInputExists
        {
            get
            {
                if (InpType == InputType.None) return true;
                return EligibleCards.Count > 0;
            }
        }

        // TODO: use this in the ui to make eligible cards glow?
        // TODO: make it only cards that are on the board by default maybe??
        public List<Card> EligibleCards => Board.Instance.AllCards.FindAll(card => _cardPredicate(card));
    }
}