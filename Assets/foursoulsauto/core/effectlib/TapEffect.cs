namespace foursoulsauto.core.effectlib
{
    // wrapper class for discharging an item as it hops on the stack
    public class TapEffect : CardEffect
    {
        public TapEffect(Card originCard, IStackEffect effect) : base(originCard, effect) { }

        public override void OnStackAdd()
        {
            base.OnStackAdd();
            OriginCard.IsCharged = false;
        }
    }
}