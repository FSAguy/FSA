using System;
using foursoulsauto.core.effectlib;
using foursoulsauto.core.player;

namespace foursoulsauto.core
{
    public class AttackRoll : DieRoll
    {
        private const int HIT_INDEX = 0;
        private const int MISS_INDEX = 1;

        private static IStackEffect[] GetAttackEffects(Player attacker, LivingCard defender)
        {
            var hitEffect = new DealDamageEffect(defender, () => attacker.Character.Attack);
            var missEffect = new DealDamageEffect(attacker.Character, () => defender.Attack);

            return new IStackEffect[] { hitEffect, missEffect };
        }
        
        private readonly LivingCard _defender;

        public AttackRoll(Player roller, LivingCard defender) : base(roller, GetAttackEffects(roller, defender))
        {
            _defender = defender;
        }
        

        protected override IStackEffect RolledEffect =>
            PotentialEffects[Result >= _defender.Evasion ? HIT_INDEX : MISS_INDEX];

        protected override int GetResultAfterMods(int roll)
        {
            return roll; //TODO
        }
    }
}