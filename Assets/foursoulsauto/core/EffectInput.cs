using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace foursoulsauto.core
{
    // way for a CardAction to request input from a player
    // TODO: maybe SingleCardTarget should be just a single MultiCardTarget?
    public enum InputType {None, SingleCardTarget, MultiCardTarget}
    public class EffectInput
    {
        private readonly Func<Card, bool> _cardPredicate;
        
        private Card _cardInput;
        
        private List<Card> _multiCardInput;

        public InputType InpType { get; }
        public bool Filled { get; private set; } 

        public Card CardInput
        {
            get => _cardInput;
            set
            {
                if (!IsCardEligible(value)) throw new Exception("Input not valid!");
                _cardInput = value;
                Filled = true;
            }
        }

        public List<Card> MultiCardInput
        {
            get => _multiCardInput;
            set
            {
                if (!value.All(IsCardEligible)) throw new Exception("Input not valid!");
                _multiCardInput = value;
                Filled = true;
            }
        }

        public int MultiCardExcpectedAmount { get; }

        public EffectInput()
        {
            InpType = InputType.None;
            Filled = true;
        }

        public EffectInput(Func<Card, bool> cardPredicate, int amount = 1)
        {
            _cardPredicate = cardPredicate;
            MultiCardExcpectedAmount = amount;
            InpType = amount == 1 ? InputType.SingleCardTarget : InputType.MultiCardTarget;
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