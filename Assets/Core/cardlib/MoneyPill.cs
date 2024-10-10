using core.effectlib;

namespace core.cardlib
{
    public class MoneyPill : Card
    {
        public override CardAction PlayAction => new MoneyPillAction(this);
        
        private class MoneyPillAction : LootCardAction
        {
            protected override IStackEffect GenerateLootEffect(Player player)
            {
                var okay = new PlayersGainCentsEffect(player, 4);
                var great = new PlayersGainCentsEffect(player, 7);
                var bad = new PlayersGainCentsEffect(player, -4);

                var effects = new IStackEffect[] { okay, okay, great, great, bad, bad };

                return new RollCreatingEffect(new NonAttackDieRoll(player), effects);
            }
        
            public MoneyPillAction(Card card) : base(card) { }
        }
    }
}