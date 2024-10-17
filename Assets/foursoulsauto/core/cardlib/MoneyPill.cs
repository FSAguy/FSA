using System.Collections.Generic;
using foursoulsauto.core.actionlib;
using foursoulsauto.core.effectlib;
using UnityEngine;

namespace foursoulsauto.core.cardlib
{
    public class MoneyPill : Card
    {
        [SerializeField] private MagicNumber amount1;
        [SerializeField] private MagicNumber amount2;
        [SerializeField] private MagicNumber amount3;
        
        private IStackEffect GenerateEffect(EffectInput input)
        {
            var player = Board.Instance.PriorityPlayer;
                
            var okay = new PlayersGainCentsEffect(player, () => amount1.Value); 
            var great = new PlayersGainCentsEffect(player, () => amount2.Value);
            var bad = new PlayersGainCentsEffect(player, () => amount3.Value);
                
            var effects = new IStackEffect[] { okay, okay, great, great, bad, bad };

            return new RollCreatingEffect(new NonAttackDieRoll(player, effects));
        }

        public override List<CardAction> Actions => new()
        {
            new LootCardAction( this, GenerateEffect )
        };
    }
}