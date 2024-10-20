using System;
using System.Collections.Generic;

namespace foursoulsauto.core
{
    // way for a CardAction to request input from a player
    // TODO: other types of input
    public enum InputType {None, SingleCardTarget}
    public class EffectInput
    {
        private readonly Func<Card, bool> _cardPredicate;
        
        public Card _cardInput;
        public InputType InpType { get; }
        public bool IsInputFilled { get; private set; } = false;

        public Card CardInput
        {
            get => _cardInput;
            set
            {
                if (!IsCardEligible(value)) throw new Exception("Input not valid!");
                _cardInput = value;
                IsInputFilled = true;
            }
        }

        public EffectInput()
        {
            InpType = InputType.None;
            IsInputFilled = true;
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