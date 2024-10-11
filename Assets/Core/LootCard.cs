using System.Collections.Generic;
using core.effectlib;

namespace core
{
    public abstract class LootCard : Card
    {
        protected abstract CardAction LootPlayAction { get;  }
        public override List<CardAction> Actions => new()
        {
            new LootCardAction(LootPlayAction)
        };

        private class LootCardAction : CardAction
        {

            private CardAction _action;

            public LootCardAction(CardAction action) : base(action.Origin)
            {
                _action = action;
            }

            public override bool MayUse => _action.MayUse;

            public override CardEffect GenerateEffect()
            {
                Board.Instance.VoidCard(Origin);
                var effect = new EffectAppender(
                    new DiscardEffect(Origin), _action.GenerateEffect());
                return new CardEffect(Origin, effect);
            }

            public override string Text => "Play Loot";
        }
    }
    
}