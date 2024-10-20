using System.Collections.Generic;
using foursoulsauto.core.actionlib;
using foursoulsauto.core.effectlib;
using UnityEngine;

namespace foursoulsauto.core.cardlib
{
    public class Bomb : Card
    {
        [SerializeField] private MagicNumber damageNum;
        public override List<CardAction> Actions => new()
        {
            new LootCardAction(this, 
                input => new DealDamageEffect(input.CardInput as LivingCard, () => damageNum.Value), 
                input:new EffectInput(card => card is LivingCard {IsAlive:true})) 
        };
    }
}