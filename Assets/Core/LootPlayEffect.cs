namespace Core
{
    public abstract class LootPlayEffect : CardEffect
    {
        public sealed override void Resolve()
        {
            LootResolve();
            Board.Instance.Discard(OriginCard);
        }

        protected abstract void LootResolve();

        public abstract override string GetEffectText();

        public override void Fizzle()
        {
            Board.Instance.Discard(OriginCard);
        }

        protected LootPlayEffect(Card originCard) : base(originCard)
        {
        }
    }
}