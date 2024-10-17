using System;
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

        public void Resolve()
        {
            _target.LootPlaysRemaining += _amount.Invoke();
        }

        public string GetEffectText()
        {
            return $"{_target.CharName} will gain {_amount} additional loot play" + (_amount.Invoke() > 1 ? "s" : "");
        }
    }
}