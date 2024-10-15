using System;
using UnityEngine;

namespace foursoulsauto.core.player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerHand hand;

        private int _cents;
        private int _lootPlaysLeft = 1; // todo: change this lol
        private int _attacksLeft = 1; // todo: change lol

        [SerializeField] private LivingCard character; //TODO: should not be serialized, its just easier for testing
        public LivingCard Character => character;
        public PlayerHand Hand => hand;

        public string CharName => Character.CardName;
        
        public event Action PlayerPassed;
        public event Action LootPlaysChanged;
        public event Action CentsChanged;

        public bool HasLootPlays => _lootPlaysLeft > 0;
        public bool HasAttacksLeft => _attacksLeft > 0;

        public int AttacksRemaining
        {
            get => _attacksLeft;
            set
            {
                _attacksLeft = value; // TODO: invoke event
            }
        }
        public int LootPlaysRemaining
        {
            get => _lootPlaysLeft;
            set
            {
                _lootPlaysLeft = value;
                LootPlaysChanged?.Invoke();
            }
        }
        
        public int Cents
        {
            get => _cents;
            set
            {
                _cents = Mathf.Max(value, 0);
                CentsChanged?.Invoke();
            }
        }

        public void Pass()
        {
            PlayerPassed?.Invoke();
        }

        public void PlayEffect(CardAction action)
        {
            Board.Instance.PlayEffect(action.GenerateEffect());
        }
        
    }
}
