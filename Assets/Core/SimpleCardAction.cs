namespace core
{
    // Simple as in there are no requirements to use it, you just do
    public class SimpleCardAction : CardAction
    {
        private readonly CardEffect _effect;
        public override bool MayUse => true; // this is the simple part

        public SimpleCardAction(Card card, IStackEffect effect, string text = "PLACEHOLDER") : base(card)
        {
            _effect = new CardEffect(card, effect);
            Text = text;
        }

        public override CardEffect GenerateEffect()
        {
            return _effect;
        }

        public override string Text { get; }
    }
}