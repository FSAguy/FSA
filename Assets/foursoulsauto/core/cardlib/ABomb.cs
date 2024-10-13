using System.Collections.Generic;
using foursoulsauto.core.effectlib;

namespace foursoulsauto.core.cardlib
{
    public class ABomb : Card
    {
        public override List<CardAction> Actions => new()
        {
            new LootCardAction(this, 
                input => new DealDamageEffect(input.LivingCardTarget, 1), // TODO: magic num
                input:new EffectInput(InputType.LivingCardTarget)) 
        };
    }
}