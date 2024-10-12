namespace foursoulsauto.core
{
    public abstract class CardAction
    {
        public readonly Card Origin;
        public CardAction(Card card)
        {
            Origin = card;
        }
        public abstract bool MayUse { get; }
        public abstract CardEffect GenerateEffect();

        public abstract string Text { get; }
    }
}