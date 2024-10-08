namespace Core.CardLibrary
{
    public class APenny : Card
    {
        public override CardAction PlayAction => new APennyAction(this);

        private class APennyAction : LootCardAction
        {
            public override LootPlayEffect GenerateLootEffect(Player player)
            {
                return new PennyEffect(Origin, player);
            }

            public APennyAction(Card card) : base(card)
            {
            }
        }

        private class PennyEffect : LootPlayEffect 
        {
            private readonly Player _target;

            public PennyEffect(Card card, Player player) : base(card)
            {
                _target = player;
            }

            protected override void LootResolve()
            {
                _target.Cents += 1;
            }

            public override string GetEffectText()
            {
                return $"{_target} receives $1";
            }
        }
    }
}