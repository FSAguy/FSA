using System;
using foursoulsauto.core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace foursoulsauto.ui
{
    public class CardUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private SpriteRenderer bodyRenderer;
        
        private Card _card;

        public event Action<Card, PointerEventData> CardClicked;
        
        public virtual void Subscribe(Card card)
        {
            _card = card;
            _card.ChangedFace += OnChangedFace;
            _card.ChangedCharge += OnChangedCharge;
        }

        private void OnChangedCharge(bool obj)
        {
            _card.TapAnim(obj);
        }

        protected virtual void OnChangedFace(bool isUp)
        {
            bodyRenderer.sprite = isUp ? _card.TopSprite : _card.BottomSprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            CardClicked?.Invoke(_card, eventData);
        }
    }
}