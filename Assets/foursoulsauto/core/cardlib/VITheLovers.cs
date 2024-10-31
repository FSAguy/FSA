using System.Collections.Generic;
using foursoulsauto.core.actionlib;
using foursoulsauto.core.effectlib;
using UnityEngine;

namespace foursoulsauto.core.cardlib
{
    public class VITheLovers : Card
    {
        [SerializeField] private MagicNumber healthNumber;

        public override List<CardAction> Actions => new()
        {
            new LootCardAction(this,
                input => new PlayerTempMaxHpEffect(input.CardInput.Owner, () => healthNumber.Value),
                input: new EffectInput(card => card.Owner != null))
        };
    }
}