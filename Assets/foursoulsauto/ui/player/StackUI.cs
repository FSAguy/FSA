using System;
using System.Collections;
using System.Collections.Generic;
using foursoulsauto.core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foursoulsauto.ui.player
{
    //TODO: Animate
    public class StackUI : PlayerUIModule
    {
        private static readonly int ItemPushedTrigger = Animator.StringToHash("Item Pushed");
        [SerializeField] private Transform stackPanel;
        [SerializeField] private GameObject descriptionBox; 
        [SerializeField] private float stackDescriptionTime;
        [SerializeField] private Animator animator;

        private TMP_Text _descriptionText;
        private Coroutine _descriptionCoroutine;

        private StackMemberUI _aboutToPop = null;
        
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
            stack.ItemAboutToPop += OnItemAboutToPop;
        }

        private void OnItemAboutToPop(IVisualStackEffect obj)
        {
            _aboutToPop =  _effectToMember[obj];
            _aboutToPop.AnimateAboutToPop();
        }

        private void OnItemFizzled(IVisualStackEffect obj)
        {
            CloseEffectDescription();
            var stackMember = _effectToMember[obj];
            _effectToMember.Remove(obj);
            Destroy(stackMember.gameObject);
            _aboutToPop = null;
        }
        
        private void OnItemPushed(IVisualStackEffect effect)
        {
            _aboutToPop?.AnimateDeflate();
            _aboutToPop = null;

            var stackMember = effect.CreateStackVisual();
            stackMember.Effect = effect;
            stackMember.PointerEntered += member =>
            {
                if (_descriptionCoroutine is not null) StopCoroutine(_descriptionCoroutine);
                _descriptionCoroutine = StartCoroutine(DisplayEffectDescription(member));
            };
            stackMember.PointerExited += _ => CloseEffectDescription();
            stackMember.transform.SetParent(stackPanel);
            _effectToMember.Add(effect, stackMember);
            animator.SetTrigger(ItemPushedTrigger);
        }

        private void CloseEffectDescription()
        {
            if (_descriptionCoroutine is not null) StopCoroutine(_descriptionCoroutine);
            descriptionBox.SetActive(false);
        }

        private IEnumerator DisplayEffectDescription(StackMemberUI stackMember)
        {
            yield return new WaitForSeconds(stackDescriptionTime);
            _descriptionText.text = stackMember.Effect.GetEffectText();
            var newPos = descriptionBox.transform.position;
            newPos.y = stackMember.transform.position.y;
            descriptionBox.transform.position = newPos;
            descriptionBox.SetActive(true);
            _descriptionCoroutine = null;
        }
    }
}