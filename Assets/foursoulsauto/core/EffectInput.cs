using System;
using System.Collections.Generic;

namespace foursoulsauto.core
{
    // way for a CardAction to request input from a player
    // TODO: other types of input
    public enum InputType {None, LivingCardTarget}
    public class EffectInput
    {
        public LivingCard LivingCardTarget;
        private readonly Func<Card, bool> _cardPredicate;
        
        public InputType InpType { get; }

        public EffectInput()
        {
            InpType = InputType.None;
        }

        public EffectInput(InputType type)
        {
            InpType = type;
            if (InpType == InputType.LivingCardTarget)
            {
                _cardPredicate = card => card is LivingCard { Hp: > 0 };
            }
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