using System.Collections.Generic;
using foursoulsauto.core.effectlib;

namespace foursoulsauto.core.cardlib
{
    public class APenny : Card
    {
        public override List<CardAction> Actions => new() {
            new LootCardAction(
                this,
                input => new PlayersGainCentsEffect(Board.Instance.PriorityPlayer, 1) 
                )
        };
    }
}