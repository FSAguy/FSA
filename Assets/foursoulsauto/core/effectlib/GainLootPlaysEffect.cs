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
            var amount = _amount.Invoke();
            return $"{_target.CharName} will gain {amount} additional loot play" + (amount > 1 ? "s" : "");
        }
    }
}