using System;
using UnityEngine;

namespace core
{
    public abstract class LivingCard : Card
    {
        public event Action<int> TookDamage;
        public event Action HpChanged;
        public event Action Died;
        
        [SerializeField] private int startingHp;
        [SerializeField] private int startingEvasion;
        [SerializeField] private int startingAttack;
        
        private int _currentHp;
        private int _currentEvasion;
        private int _currentAttack;

        // Never change CurrentHp when "taking damage", use TakeDamage for that
        // Change CurrentHp when healing or modifying hp without damage
        public int CurrentHp
        {
            get => _currentHp;
            set
            {
                _currentHp = Mathf.Max(value, 0);
                HpChanged?.Invoke();
                if (CurrentHp == 0) Died?.Invoke();
            }
        }

        // TODO: modified max hp
        protected override void Awake()
        {
            base.Awake();
            Died += OnDeath;
        }

        protected virtual void OnDeath()
        {
            Discard();
        }

        public void TakeDamage(int dmg)
        {
            CurrentHp -= dmg;
            if (dmg > 0) TookDamage?.Invoke(dmg);
        }

        public void HealToFull()
        {
            CurrentHp = startingHp;
        }
    }
}