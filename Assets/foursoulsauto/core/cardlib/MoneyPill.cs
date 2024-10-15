using System.Collections.Generic;
using foursoulsauto.core.effectlib;

namespace foursoulsauto.core.cardlib
{
    public class MoneyPill : Card
    {
        private IStackEffect GenerateEffect(EffectInput input)
        {
            var player = Board.Instance.PriorityPlayer;
                
            var okay = new PlayersGainCentsEffect(player, 4); // TODO: magic nums
            var great = new PlayersGainCentsEffect(player, 7);
            var bad = new PlayersGainCentsEffect(player, -4);
                
            var effects = new IStackEffect[] { okay, okay, great, great, bad, bad };

            return new RollCreatingEffect(new NonAttackDieRoll(player, effects));
        }

        public override List<CardAction> Actions => new()
        {
            new LootCardAction( this, GenerateEffect )
        };
    }
}