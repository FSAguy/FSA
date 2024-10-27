using System;
using foursoulsauto.core.cardlib;
using foursoulsauto.core.effectlib;
using UnityEngine;

namespace foursoulsauto.core.player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerHand hand;
        [SerializeField] private GridCardContainer activeCardZone;

        private int _cents;
        private int _lootPlaysLeft = 1; // todo: change this lol
        private int _attacksLeft = 1; // todo: change lol
        private bool _hasPriority;
        private bool _isActive;
        private CharacterCard _character;

        public PlayerHand Hand => hand;
        public GridCardContainer ActiveCardZone => activeCardZone;

        public string CharName => Character.CardName;

        public event Action<EffectInput> RequestedInput;

        public event Action StateChanged;
        
        public event Action PlayerPassed;
        public event Action LootPlaysChanged;
        public event Action AttacksLeftChanged;
        public event Action CentsChanged;
        public event Action PriorityChanged;
        public event Action ActiveChanged;

        public bool HasLootPlays => _lootPlaysLeft > 0;
        public bool HasAttacksLeft => _attacksLeft > 0;

        public CharacterCard Character
        {
            get => _character;
            set
            {
                if (_character != null)
                {
                    _character.Discard();
                }
                // TODO: should insert into the starting position, requires changing cardContainer (good)
                _character = value;
                activeCardZone.MoveInto(_character);
            }
        }
        public bool HasPriority
        {
            get => _hasPriority;
            set
            {
                if (_hasPriority == value) return;
                _hasPriority = value;
                PriorityChanged?.Invoke();
                StateChanged?.Invoke();
            }
        }
        
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                ActiveChanged?.Invoke();
                StateChanged?.Invoke();
            }
        }

        public int AttacksRemaining
        {
            get => _attacksLeft;
            set
            {
                _attacksLeft = value; 
                AttacksLeftChanged?.Invoke();
                StateChanged?.Invoke();
            }
        }
        
        public int LootPlaysRemaining
        {
            get => _lootPlaysLeft;
            set
            {
                _lootPlaysLeft = value;
                LootPlaysChanged?.Invoke();
                StateChanged?.Invoke();
            }
        }

        public bool MayAttack => 
            Board.Instance.ActivePlayer == this && 
            Board.Instance.Stack.IsEmpty && 
            HasAttacksLeft && 
            Character is not null &&
            Character.IsAlive; 
        
        public int Cents
        {
            get => _cents;
            set
            {
                _cents = Mathf.Max(value, 0);
                CentsChanged?.Invoke();
                StateChanged?.Invoke();
            }
        }

        public void Pass()
        {
            PlayerPassed?.Invoke();
            StateChanged?.Invoke();
        }

        public void PlayEffect(CardAction action)
        {
            Board.Instance.AddEffect(action.GenerateEffect());
        }

        public void RequestInput(EffectInput request)
        {
            RequestedInput?.Invoke(request);
        }

        public void GainItem(Card item)
        {
            activeCardZone.MoveInto(item);
        }
        
        public void DeclareAttack()
        {
            Board.Instance.AddEffect(new AttackDeclarationEffect(this));
        }
    }
}
