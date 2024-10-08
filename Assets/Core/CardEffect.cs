using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public abstract class CardEffect : IStackEffect
    {
        private static readonly GameObject StackMemberClone = 
            Resources.Load<GameObject>("Prefabs/UI/CardEffectStackMember");
        protected readonly Card OriginCard;

        protected CardEffect(Card originCard)
        {
            OriginCard = originCard;
        }

        public abstract void Resolve();

        public abstract string GetEffectText();

        public virtual void Fizzle()
        {
        }

        public GameObject GetStackVisual()
        {
            var stackMember = Object.Instantiate(StackMemberClone);
            var cardImage = stackMember.transform.Find("CardImage").GetComponent<Image>();
            cardImage.sprite = OriginCard.CurrentSprite;
            
            return stackMember;
        }
    }
}