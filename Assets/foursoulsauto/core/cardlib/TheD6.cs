using System.Collections.Generic;
using foursoulsauto.core.actionlib;
using foursoulsauto.core.effectlib;

namespace foursoulsauto.core.cardlib
{
    public class TheD6 : Card
    {
        public override List<CardAction> Actions => new()
        {
            new TapAction(this,
                _ => new RerollEffect(
                    Board.Instance.Stack.GetFirstWhere(effect => effect is DieRoll) as DieRoll
                ), // TODO: the text says "SELECT" a dice roll... which means it doesnt have to be the top one
                // this is a problem for later :)))
                "Reroll a dice",
                () => Board.Instance.Stack.GetFirstWhere(effect => effect is DieRoll) is not null
                )
        };
    }
}
