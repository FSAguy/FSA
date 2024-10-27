using System;
using foursoulsauto.core;
using UnityEngine;
using UnityEngine.EventSystems;

namespace foursoulsauto.ui
{
    public class StackMemberUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<StackMemberUI, PointerEventData> PointerEntered;
        public event Action<StackMemberUI, PointerEventData> PointerExited;
        
        public IVisualStackEffect Effect { get; set; }

        public void OnPointerEnter(PointerEventData eventData)
        {
            PointerEntered?.Invoke(this, eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            PointerExited?.Invoke(this, eventData);
        }
    }
}