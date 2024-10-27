using System;
using System.Collections.Generic;
using foursoulsauto.core;
using TMPro;
using UnityEngine;

namespace foursoulsauto.ui.player
{
    //TODO: Animate
    public class StackUI : PlayerUIModule
    {
        [SerializeField] private Transform stackPanel;
        [SerializeField] private GameObject descriptionBox; 
        [SerializeField] private float stackDescriptionTime;

        private TMP_Text _descriptionText;
        
        private readonly Dictionary<IVisualStackEffect, StackMemberUI> _effectToMember = new();

        private void Awake()
        {
            _descriptionText = descriptionBox.GetComponentInChildren<TMP_Text>();
        }

        protected override void OnClose()
        {
            stackPanel.gameObject.SetActive(false);
            descriptionBox.SetActive(false);
        }

        protected override void OnOpen()
        {
            stackPanel.gameObject.SetActive(true);
        }

        protected override void Start()
        {
            base.Start();
            var stack = Board.Instance.Stack;
            stack.ItemPushed += OnItemPushed;
            stack.ItemFizzled += OnItemFizzled;
            stack.ItemResolved += OnItemFizzled; //TODO: make fizzled and resolved different (cool disintegration effect)
        }
                    
        private void OnItemFizzled(IVisualStackEffect obj)
        {
            var stackMember = _effectToMember[obj];
            _effectToMember.Remove(obj);
            Destroy(stackMember.gameObject);
        }
        
        private void OnItemPushed(IVisualStackEffect effect)
        {
            var stackMember = effect.CreateStackVisual();
            stackMember.Effect = effect;
            stackMember.PointerEntered += (member, data) => DisplayEffectDescription(member, data.position);
            stackMember.PointerExited += (_, _) => CloseEffectDescription();
            stackMember.transform.SetParent(stackPanel);
            _effectToMember.Add(effect, stackMember);
        }

        private void CloseEffectDescription()
        {
            descriptionBox.SetActive(false);
        }

        private void DisplayEffectDescription(StackMemberUI stackMember, Vector3 pos)
        {
            _descriptionText.text = stackMember.Effect.GetEffectText();
            descriptionBox.transform.position = pos;
            descriptionBox.SetActive(true);
        }
    }
}