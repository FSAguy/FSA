using System.Collections;
using foursoulsauto.ui;
using UnityEngine;
using UnityEngine.UI;

namespace foursoulsauto.core
{
    public class CardEffect : IVisualStackEffect
    {
        private static readonly StackMemberUI StackMemberClone = 
            Resources.Load<StackMemberUI>("Prefabs/UI/CardEffectStackMember");
        public readonly Card OriginCard;
        private readonly IStackEffect _effect;

        public CardEffect(Card originCard, IStackEffect effect) 
        {
            OriginCard = originCard;
            _effect = effect;
        }

        public virtual void OnStackAdd()
        {
            _effect.OnStackAdd();
        }

        public virtual IEnumerator Resolve() => _effect.Resolve();

        public string GetEffectText() => _effect.GetEffectText();

        public virtual void OnLeaveStack() => _effect.OnLeaveStack();

        public StackMemberUI CreateStackVisual()
        {
            var stackMember = Object.Instantiate(StackMemberClone);
            var cardImage = stackMember.transform.Find("FrameMask/CardImage").GetComponent<Image>();
            cardImage.sprite = OriginCard.TopSprite;
            
            return stackMember;
        }
    }
}