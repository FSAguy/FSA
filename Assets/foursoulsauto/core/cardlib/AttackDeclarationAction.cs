using System;
using foursoulsauto.core.effectlib;

namespace foursoulsauto.core.cardlib
{
    public class AttackDeclarationAction : CardAction
    {
        public AttackDeclarationAction(LivingCard origin) : 
            base(origin, 
                
                input => new AttackDeclarationEffect(Board.Instance.ActivePlayer, origin),
                
                 "Attack", 
                
                 () => Board.Instance.ActivePlayer.HasAttacksLeft && Board.Instance.Stack.IsEmpty,
                
                new EffectInput()
                ) { }
    }
}