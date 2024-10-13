using System;
using foursoulsauto.core.effectlib;

namespace foursoulsauto.core
{
    public class LootCardAction : CardAction
    {
        public LootCardAction(Card origin, Func<EffectInput, IStackEffect> generatorFunc, 
            Func<bool> mayUseDelegate = null, EffectInput input = null) : 
            base(origin, generatorFunc, "Play Loot", mayUseDelegate, input) { }


        public override bool MayUse => base.MayUse && Board.Instance.PriorityPlayer.HasLootPlays;

        public override CardEffect GenerateEffect()
        {
            return new LootCardPlayEffect(Origin, base.GenerateEffect());
        }
    }
}