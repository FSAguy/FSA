using System.Collections.Generic;
using foursoulsauto.core.actionlib;
using foursoulsauto.core.effectlib;
using UnityEngine;

namespace foursoulsauto.core.cardlib
{
    // 1 cent, 2 cent, etc...
    // just make sure to set the magic nums correctly
    public class GainCentCard : Card
    {
        [SerializeField] private MagicNumber centAmount;
        
        public override List<CardAction> Actions => new() {
            new LootCardAction(
                this,
                input => new PlayersGainCentsEffect(Board.Instance.PriorityPlayer, centAmount.Value) 
                )
        };
    }
}