using System;
using UnityEngine;

namespace foursoulsauto.core
{
    public abstract class LivingCard : Card
    {
        public event Action<int> TookDamage;
        public event Action<int> HpChanged;
        public event Action Died;

        [SerializeField] private int startingHp;
        [SerializeField] private int startingEvasion;
        [SerializeField] private int startingAttack;
        
        private int _currentHp;
        private int _currentEvasion;
        private int _currentAttack;
        private bool _attackable; // as in, able to be attacked

        // Never change CurrentHp when "taking damage", use TakeDamage for that
        // Change CurrentHp when healing or modifying hp without damage
        public int CurrentHp
        {
            get => _currentHp;
            set
            {
                _currentHp = Mathf.Max(value, 0);
                HpChanged?.Invoke(_currentHp);
                if (CurrentHp == 0) Died?.Invoke();
            }
        }

        protected override void Awake()
        {
            base.Awake();
            Died += OnDeath;
            ResetStats();
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
            // TODO: modified max hp (like +1hp treasures)
            CurrentHp = startingHp;
        }

        public void ResetStats()
        {
            CurrentHp = startingHp;
            // TODO: the other stats
        }
    }
}