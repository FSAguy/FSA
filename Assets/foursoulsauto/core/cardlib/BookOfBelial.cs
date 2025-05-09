using System.Collections.Generic;
using foursoulsauto.core.actionlib;
using foursoulsauto.core.effectlib;
using UnityEngine;

namespace foursoulsauto.core.cardlib
{
    public class BookOfBelial : Card
    {
        [SerializeField] private MagicNumber rollChangeNum;

        public override List<CardAction> Actions => new()
        {
            new TapAction(this,
                _ => new AddToRollEffect(
                    Board.Instance.Stack.TopRollEffect,
                    () => rollChangeNum.Value
                ),
                $"Add {rollChangeNum.Value} to a roll",
                () => Board.Instance.Stack.HasRoll
            ),
            new TapAction(this,
                _ => new AddToRollEffect(
                    Board.Instance.Stack.TopRollEffect,
                    () => -rollChangeNum.Value
                ),
                $"Subtract {rollChangeNum.Value} from a roll",
                () => Board.Instance.Stack.HasRoll
            ),
        };
    }
}