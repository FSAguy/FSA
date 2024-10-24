using System;
using foursoulsauto.core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace foursoulsauto.ui
{
    public class CardUI : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private SpriteRenderer bodyRenderer;
        
        protected Card TargetCard;

        public event Action<Card, PointerEventData> CardClicked;

        protected virtual void Awake()
        {
            TargetCard = GetComponent<Card>();
            TargetCard.ChangedFace += OnChangedFace;
            TargetCard.ChangedCharge += OnChangedCharge;
        }

        private void OnChangedCharge(bool obj)
        {
            TargetCard.TapAnim(obj);
        }

        protected virtual void OnChangedFace(bool isUp)
        {
            bodyRenderer.sprite = isUp ? TargetCard.TopSprite : TargetCard.BottomSprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            CardClicked?.Invoke(TargetCard, eventData);
        }
    }
}