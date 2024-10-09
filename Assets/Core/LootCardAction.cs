using core.effectlib;

namespace core
{
    public abstract class LootCardAction : CardAction
    {

        public sealed override CardEffect GenerateEffect(Player player)
        {
            Board.Instance.VoidCard(Origin);
            var effect = new EffectAppender(
                new DiscardEffect(Origin), GenerateLootEffect(player));
            return new CardEffect(Origin, effect);
        }

        protected abstract IEffect GenerateLootEffect(Player player);

        protected LootCardAction(Card card) : base(card)
        {
        }
    }
}