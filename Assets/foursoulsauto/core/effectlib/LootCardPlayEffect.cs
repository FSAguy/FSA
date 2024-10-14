using foursoulsauto.core.player;

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
            Board.Instance.PriorityPlayer.LootPlaysRemaining--;
            base.OnStackAdd();
        }

        public override void Resolve()
        {
            OriginCard.Discard();
            base.Resolve();
        }


        public override void Fizzle()
        {
            OriginCard.Discard();
            base.Fizzle();
        }
    }
}