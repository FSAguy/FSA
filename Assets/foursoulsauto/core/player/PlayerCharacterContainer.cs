using System;
using foursoulsauto.ui;
using UnityEngine;

namespace foursoulsauto.core.player
{
    public class PlayerCharacterContainer : PlayerCardContainer
    {
        [SerializeField] private Card _charCard; // TODO: remove post testing
        
        public override ContainerType ConType => ContainerType.Character;

        protected override void Awake() // TODO: see above todo
        {
            base.Awake();
            MoveInto(_charCard);
        }

        protected override void Add(Card card)
        {
            if (card is not LivingCard livingCard) throw new Exception("character must have stat block");
            base.Add(livingCard);
            livingCard.MoveTo(transform);
            Owner.Character = livingCard;
        }
    }
}