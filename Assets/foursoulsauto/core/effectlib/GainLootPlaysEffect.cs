using System;
using System.Collections;
using foursoulsauto.core.player;

namespace foursoulsauto.core.effectlib
{
    public class GainLootPlaysEffect : IStackEffect
    {
        private Player _target;
        private Func<int> _amount;

        public GainLootPlaysEffect(Player target, Func<int> amount)
        {
            _target = target;
            _amount = amount;
        }

        public IEnumerator Resolve()
        {
            _target.LootPlaysRemaining += _amount.Invoke();
            yield return null;
        }

        public string GetEffectText()
        {
            return $"{_target.CharName} will gain {_amount} additional loot play" + (_amount.Invoke() > 1 ? "s" : "");
        }
    }
}