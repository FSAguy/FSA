namespace foursoulsauto.core.effectlib
{
    // wrapper effect for playing a loot card
    public class LootCardPlayEffect : CardEffect
    {
        private readonly CardEffect _effect;

        public LootCardPlayEffect(Card originCard, IStackEffect effect) : base(originCard, effect) { }

        public override void OnStackAdd()
        {
            Board.Instance.VoidCard(OriginCard);
            base.OnStackAdd();
        }

        public override void Fizzle()
        {
            OriginCard.Discard();
            base.Fizzle();
        }
    }
}