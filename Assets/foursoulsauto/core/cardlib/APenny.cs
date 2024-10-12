using foursoulsauto.core.effectlib;

namespace foursoulsauto.core.cardlib
{
    public class APenny : LootCard
    {
        protected override CardAction LootPlayAction => 
            new SimpleCardAction(this, new PlayersGainCentsEffect(Board.Instance.PriorityPlayer, 1));

    }
}