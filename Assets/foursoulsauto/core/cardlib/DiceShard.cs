using System.Collections.Generic;
using foursoulsauto.core.effectlib;

namespace foursoulsauto.core.cardlib
{
    public class DiceShard : Card
    {
        public override List<CardAction> Actions => new()
        {
            new LootCardAction(
                this,
                input => new RerollEffect(
                    Board.Instance.Stack.GetFirstWhere(effect => effect is DieRoll) as DieRoll
                ), // TODO: the text says "SELECT" a dice roll... which means it doesnt have to be the top one
                   // this is a problem for later :)
                () => Board.Instance.Stack.GetFirstWhere(effect => effect is DieRoll) is not null
            )
        };
    }
}