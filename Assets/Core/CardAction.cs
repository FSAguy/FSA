namespace core
{
    public abstract class CardAction
    {
        protected readonly Card Origin;
        public CardAction(Card card)
        {
            Origin = card;
        }
        public virtual bool MayUse => true;
        public abstract CardEffect GenerateEffect(Player player);
        
    }
}