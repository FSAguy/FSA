namespace foursoulsauto.core.effectlib
{
    // wrapper effect for playing a loot card
    public class LootCardPlayEffect : CardEffect
    {
        public LootCardPlayEffect(Card originCard, IStackEffect effect) : base(originCard, effect) { }

        public override void OnStackAdd()
        {
            Board.Instance.VoidCard(OriginCard);
            Board.Instance.PriorityPlayer.LootPlaysRemaining--;
            base.OnStackAdd();
        }

        public override void OnLeaveStack()
        {
            base.OnLeaveStack();
            OriginCard.Discard();
        }
    }
}