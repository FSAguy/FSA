namespace Core
{
    public abstract class LootCardAction : CardAction
    {
        protected LootCardAction(Card card) : base(card)
        {
        }

        public sealed override CardEffect GenerateEffect(Player player)
        {
            Board.Instance.VoidCard(Origin);
            return GenerateLootEffect(player);
        }

        public abstract LootPlayEffect GenerateLootEffect(Player player);

    }
}