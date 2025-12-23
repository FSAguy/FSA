using System;
using System.Collections;
using System.Collections.Generic;
using foursoulsauto.core;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

namespace foursoulsauto.ui.player
{
    public class StackUI : PlayerUIModule
    {
        [SerializeField] private RectTransform stackPanel;
        [SerializeField] private GameObject descriptionBox; 
        [SerializeField] private float stackDescriptionTime;
        [SerializeField] private VerticalLayoutGroup stackLayoutGroup;

        [SerializeField] private float slideTime = 0.2f;
        
        private TMP_Text _descriptionText;
        private Coroutine _descriptionCoroutine;
        private Coroutine _layoutTurnOn;

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
            stackLayoutGroup.enabled = false;
            CloseEffectDescription();
            var stackMember = _effectToMember[obj];
            _effectToMember.Remove(obj);
            stackMember.transform.SetParent(null);
            Destroy(stackMember.gameObject);
            _aboutToPop = null;
            RebuildAnimated(goingUp:false);
        }
        
        private void OnItemPushed(IVisualStackEffect effect)
        {
            stackLayoutGroup.enabled = false;
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
            
            var localPos = new Vector2(stackPanel.rect.width / 2, -stackPanel.rect.height * 1.25f);
            stackMember.rectTransform.anchoredPosition = localPos;
            Debug.Log(localPos);
            
            _effectToMember.Add(effect, stackMember);
            
            RebuildAnimated(goingUp:true);
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

        private void RebuildAnimated(bool goingUp)
        {
            if (_layoutTurnOn is not null) StopCoroutine(_layoutTurnOn);
            
            var prevPositions = new Dictionary<RectTransform, Vector2>();
            foreach (RectTransform child in stackPanel)
                prevPositions.Add(child, child.anchoredPosition);
            
            stackLayoutGroup.enabled = true;
            LayoutRebuilder.ForceRebuildLayoutImmediate(stackPanel);
            
            var targetPositions = new Dictionary<RectTransform, Vector2>();
            foreach (RectTransform child in stackPanel)
            {
                
                Debug.Log(child.anchoredPosition);
                targetPositions[child] = child.anchoredPosition;
            }

            stackLayoutGroup.enabled = false;

            foreach (var pair in targetPositions)
            {
                pair.Key.anchoredPosition = prevPositions[pair.Key];
                LeanTween.cancel(pair.Key);
                var tween = LeanTween.move(pair.Key, pair.Value, slideTime);
                tween.setEase(goingUp ? LeanTweenType.easeInOutExpo : LeanTweenType.easeOutBounce);
            }
            
            _layoutTurnOn = StartCoroutine(turnLayoutBackOn(slideTime));
        } 
        
        private IEnumerator turnLayoutBackOn(float time)
        {
            yield return new WaitForSeconds(time);
            stackLayoutGroup.enabled = true;
        }
    }
}