using System;
using System.Collections.Generic;
using foursoulsauto.core.actionlib;
using foursoulsauto.core.cardlib;
using UnityEngine;
using UnityEngine.Serialization;

namespace foursoulsauto.core
{
    public abstract class LivingCard : Card
    {
        public event Action<int> TookDamage;
        public event Action<int> HpChanged;
        public event Action<int> EvasionChanged;
        public event Action<int> AttackChanged;
        public event Action Died;

        [SerializeField] private int startingHp;
        [SerializeField] private int startingEvasion;
        [SerializeField] private int startingAttack;
        [SerializeField] private bool attackable; // as in able to be attacked 

        private int _hp;
        private int _evasion;
        private int _attack;

        public bool IsDead => Hp <= 0 || !IsShown; // TODO: change IsShown to IsInPlay
        public bool IsAlive => !IsDead;
        public bool IsAttackable => attackable && IsAlive;

        // Never change CurrentHp when "taking damage", use TakeDamage for that
        // Change CurrentHp when healing or modifying hp without damage
        // TODO: If a player or monster has 0HP at any point, and their death is not already on the stack,
        //       their death is put on the stack the next time any player would receive priority. 
        //       (death should not be added immediately! only after passing!)
        public int Hp
        {
            get => _hp;
            set
            {
                _hp = Mathf.Max(value, 0);
                HpChanged?.Invoke(_hp);
                if (Hp == 0) Died?.Invoke();
            }
        }

        public int Attack // TODO: include modifiers 
        {
            get => _attack;
            set
            {
                _attack = Mathf.Max(value, 0);
                AttackChanged?.Invoke(_attack);
            }
        }
        public int Evasion // TODO: include modifiers 
        {
            get => _evasion;
            set 
            {
                _evasion = Mathf.Clamp(value, 1, 7); 
                EvasionChanged?.Invoke(_evasion);
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
            Hp -= dmg;
            if (dmg > 0) TookDamage?.Invoke(dmg);
        }

        public void HealToFull()
        {
            // TODO: modified max hp (like +1hp treasures)
            Hp = startingHp;
        }

        public void ResetStats()
        {
            Hp = startingHp;
            Attack = startingAttack;
            Evasion = startingEvasion;
        }
    }
}