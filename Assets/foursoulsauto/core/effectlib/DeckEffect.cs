using System.Collections;
using foursoulsauto.core.deck;
using foursoulsauto.ui;
using UnityEngine;
using UnityEngine.UI;

namespace foursoulsauto.core.effectlib
{
    // drawing, refilling slots and such
    public abstract class DeckEffect : IVisualStackEffect
    {
        private static readonly StackMemberUI StackMemberClone = 
            Resources.Load<StackMemberUI>("Prefabs/UI/DeckEffectStackMember");
        // TODO: use stack visual
        protected DeckBehaviour DeckBehaviour;

        protected DeckEffect(DeckBehaviour deckBehaviour)
        {
            DeckBehaviour = deckBehaviour;
        }

        public abstract IEnumerator Resolve();

        public abstract string GetEffectText();

        public StackMemberUI CreateStackVisual() 
        {
            var stackMember = Object.Instantiate(StackMemberClone);
            stackMember.texture = Board.Instance.GetCardback(DeckBehaviour.DeckType).texture;
                        
            return stackMember;
        }
    }
}