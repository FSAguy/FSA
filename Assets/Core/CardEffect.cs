using UnityEngine;
using UnityEngine.UI;

namespace core
{
    public class CardEffect : IVisualStackEffect
    {
        private static readonly GameObject StackMemberClone = 
            Resources.Load<GameObject>("Prefabs/UI/CardEffectStackMember");
        public readonly Card OriginCard;
        private readonly IStackEffect _effect;

        public CardEffect(Card originCard, IStackEffect effect) 
        {
            OriginCard = originCard;
            _effect = effect;
        }

        public void OnStackAdd()
        {
            _effect.OnStackAdd();
        }

        public void Resolve() => _effect.Resolve();

        public string GetEffectText() => _effect.GetEffectText();

        public void Fizzle() => _effect.Fizzle();

        public GameObject GetStackVisual()
        {
            var stackMember = Object.Instantiate(StackMemberClone);
            var cardImage = stackMember.transform.Find("CardImage").GetComponent<Image>();
            cardImage.sprite = OriginCard.TopSprite;
            
            return stackMember;
        }
    }
}