using foursoulsauto.core;
using UnityEngine;

namespace foursoulsauto.ui
{
    public class CardUI : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer bodyRenderer;
        
        //TODO: magic nums, as in numbers that can be modified with "magic marker"
        private Card _card;
        public virtual void Subscribe(Card card)
        {
            _card = card;
            _card.ChangedFace += OnChangedFace;
        }

        protected virtual void OnChangedFace(bool isUp)
        {
            bodyRenderer.sprite = isUp ? _card.TopSprite : _card.BottomSprite;
        }
    }
}