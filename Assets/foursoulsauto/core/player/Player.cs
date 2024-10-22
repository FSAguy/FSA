using System;
using foursoulsauto.core.effectlib;
using UnityEngine;

namespace foursoulsauto.core.player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerHand hand;

        private int _cents;
        private int _lootPlaysLeft = 1; // todo: change this lol
        private int _attacksLeft = 1; // todo: change lol
        private bool _hasPriority;
        public LivingCard Character { get; set; }
        public PlayerHand Hand => hand;

        public string CharName => Character.CardName;

        public event Action<EffectInput> RequestedInput;
        
        public event Action PlayerPassed;
        public event Action LootPlaysChanged;
        public event Action AttacksLeftChanged;
        public event Action CentsChanged;
        public event Action PriorityChanged;

        public bool HasLootPlays => _lootPlaysLeft > 0;
        public bool HasAttacksLeft => _attacksLeft > 0;

        public bool HasPriority
        {
            get => _hasPriority;
            set
            {
                _hasPriority = value;
                PriorityChanged?.Invoke();
            }
        }


        public int AttacksRemaining
        {
            get => _attacksLeft;
            set
            {
                _attacksLeft = value; 
                AttacksLeftChanged?.Invoke();
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

        public bool MayAttack => // TODO: might be modified by game phases?
            Board.Instance.ActivePlayer == this && Board.Instance.Stack.IsEmpty && HasAttacksLeft && Character.IsAlive; 
        
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
            Board.Instance.AddEffect(action.GenerateEffect());
        }

        public void RequestInput(EffectInput request)
        {
            RequestedInput?.Invoke(request);
        }

        public void DeclareAttack()
        {
            Board.Instance.AddEffect(new AttackDeclarationEffect(this));
        }
    }
}
