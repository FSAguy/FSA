using foursoulsauto.core.effectlib;

namespace foursoulsauto.core.cardlib
{
    public class MoneyPill : LootCard
    {
        protected override CardAction LootPlayAction
        {
            get
            {
                var player = Board.Instance.PriorityPlayer;
                
                var okay = new PlayersGainCentsEffect(player, 4);
                var great = new PlayersGainCentsEffect(player, 7);
                var bad = new PlayersGainCentsEffect(player, -4);
                
                var effects = new IStackEffect[] { okay, okay, great, great, bad, bad };
                
                return new SimpleCardAction(this,
                    new RollCreatingEffect(new NonAttackDieRoll(player), effects)
                    );
            }
        }

    }
}