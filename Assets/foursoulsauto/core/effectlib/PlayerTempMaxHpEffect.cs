using System;
using System.Collections;
using foursoulsauto.core.player;

namespace foursoulsauto.core.effectlib
{
    public class PlayerTempMaxHpEffect : UntilEndOfTurnEffect
    {
        private readonly Player _target;
        private readonly Func<int> _amountFunc;
        
        private int _realisedAmount;

        public PlayerTempMaxHpEffect(Player target, Func<int> amountFunc)
        {
            _target = target;
            _amountFunc = amountFunc;
        }
        
        protected override IEnumerator ApplyEndOfTurnEffect()
        {
            _realisedAmount = _amountFunc.Invoke();
            _target.Character.MaxHp += _realisedAmount;
            yield break;
        }
        
        protected override void EndOfTurnCleanup()
        {
            _target.Character.MaxHp -= _realisedAmount;
        }
        
        public override string GetEffectText()
        {
            return $"Will increase {_target.CharName}'s max health by {_amountFunc.Invoke()} until end of turn.";
        }
    }
}