using UnityEngine;
using UnityEngine.UI;

namespace core
{
    public class CardEffect : IStackEffect
    {
        private static readonly GameObject StackMemberClone = 
            Resources.Load<GameObject>("Prefabs/UI/CardEffectStackMember");
        public readonly Card OriginCard;
        private readonly IEffect _effect;

        public CardEffect(Card originCard, IEffect effect) 
        {
            OriginCard = originCard;
            _effect = effect;
        }

        public void Resolve() => _effect.Resolve();

        public string GetEffectText() => _effect.GetEffectText();

        public void Fizzle() => _effect.Fizzle();

        public GameObject GetStackVisual()
        {
            var stackMember = Object.Instantiate(StackMemberClone);
            var cardImage = stackMember.transform.Find("CardImage").GetComponent<Image>();
            cardImage.sprite = OriginCard.CurrentSprite;
            
            return stackMember;
        }
    }
}