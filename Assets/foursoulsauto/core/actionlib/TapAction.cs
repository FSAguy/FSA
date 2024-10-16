using System;
using foursoulsauto.core.effectlib;

namespace foursoulsauto.core.cardlib
{
    public class TapAction : CardAction
    {
        public TapAction(Card origin, Func<EffectInput, IStackEffect> generatorFunc, string text, 
            Func<bool> mayUseDelegate = null, EffectInput input = null) : 
            base(origin, generatorFunc, text, mayUseDelegate, input) { }

        public override bool MayUse => base.MayUse && Origin.IsCharged;

        public override CardEffect GenerateEffect()
        {
            return new TapEffect(Origin, base.GenerateEffect());
        }

        public override string Text => "Tap: " + base.Text;
    }
}