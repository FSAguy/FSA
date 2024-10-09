using core.effectlib;

namespace core.cardlib
{
    public class APenny : Card
    {
        public override CardAction PlayAction => new APennyAction(this);

        private class APennyAction : LootCardAction
        {
            protected override IEffect GenerateLootEffect(Player player)
            {
                return new PlayersGainCentsEffect(player, 1);
            }

            public APennyAction(Card card) : base(card)
            {
            }
        }
    }
}