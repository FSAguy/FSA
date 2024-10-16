using foursoulsauto.core.player;

namespace foursoulsauto.core.effectlib
{
    public class GainLootPlaysEffect : IStackEffect
    {
        private Player _target;
        private int _amount;

        public GainLootPlaysEffect(Player target, int amount)
        {
            _target = target;
            _amount = amount;
        }

        public void Resolve()
        {
            _target.LootPlaysRemaining += _amount;
        }

        public string GetEffectText()
        {
            return $"{_target.CharName} will gain {_amount} additional loot play" + (_amount > 1 ? "s" : "");
        }
    }
}