using System.Collections.Generic;
using foursoulsauto.core.actionlib;
using foursoulsauto.core.effectlib;

namespace foursoulsauto.core.cardlib
{
    // most common character that just gets an extra loot play as a tap
    public class PlayLootCharacter : LivingCard
    {
        public override List<CardAction> Actions => new()
        {
            new TapAction(this,
                input => new GainLootPlaysEffect(Board.Instance.PriorityPlayer, () => 1),
                "gain loot play")
        };
    }
}