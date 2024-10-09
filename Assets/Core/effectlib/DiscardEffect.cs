namespace core.effectlib
{
    public class DiscardEffect : IEffect
    {
        private readonly Card _card;
        private readonly bool _explicitText;

        public DiscardEffect(Card card, bool explicitText = false)
        {
            _card = card;
            _explicitText = explicitText;
        }

        public void Resolve()
        {
            _card.Discard();
        }

        public string GetEffectText()
        {
            return _explicitText ? $"discards {_card.name}" : IEffect.NO_TEXT;
        }
    }
}