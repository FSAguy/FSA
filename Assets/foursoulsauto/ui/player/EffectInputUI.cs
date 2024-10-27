using System;
using System.Collections.Generic;
using foursoulsauto.core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foursoulsauto.ui.player
{
    // TODO: more types of selections such as arranging stack order or selecting "choose one" effects
    public class EffectInputUI : PlayerUIModule
    {
        [SerializeField] private GameObject topUI;
        [SerializeField] private GameObject clickThroughInputScreen; // TODO: use Find on topUI instead?
        [SerializeField] private GameObject multiCardSelectScreen;
        [SerializeField] private GameObject multiCardContentPanel;
        [SerializeField] private Button multiButtonClone;
        [SerializeField] private TMP_Text header;
        [SerializeField] private Button confirmButton;
        [SerializeField] private Button cancelButton;

        private List<GameObject> _subUIs;
        private Card _singleCardSelection;
        private List<Card> _multiCardsSelected;

        private EffectInput _currentRequest;
        

        private void Awake()
        {
            _subUIs = new List<GameObject> { clickThroughInputScreen, multiCardSelectScreen };
        }

        protected override void OnClose()
        {
            topUI.SetActive(false);
            foreach (Transform child in multiCardContentPanel.transform) Destroy(child.gameObject);
            confirmButton.interactable = false; 
            // TODO: add cancel
            _subUIs.ForEach(sui => sui.SetActive(false)); 
        }
        

        protected override void OnOpen()
        {
            topUI.SetActive(true);
        }

        protected override void Start()
        {
            base.Start();
            Manager.CardClicked += (card, _) => SelectSingleCard(card);
        }

        public void GetPlayerInput(EffectInput request)
        {
            if (!Open) return;
            header.text = "Select Target"; // TODO: customized message for every input - should probably be included in request

            confirmButton.interactable = false;
            _currentRequest = request;
            switch (_currentRequest.InpType)
            {
                case InputType.None:
                    DeclareDone();
                    break;
                case InputType.SingleCardTarget:
                    BeginSingleTarget();
                    break;
                case InputType.MultiCardTarget:
                    BeginCardList();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        private void BeginSingleTarget()
        {
            _singleCardSelection = null;
            clickThroughInputScreen.SetActive(true);
        }

        private void BeginCardList()
        {
            multiCardSelectScreen.SetActive(true);
            foreach (var card in _currentRequest.EligibleCards)
            {
                var clone = Instantiate(multiButtonClone, multiCardContentPanel.transform);
                clone.GetComponent<Image>().sprite = card.TopSprite;
                clone.onClick.AddListener(delegate { AddMultiCardSelection(card, clone.gameObject); }); 
            }

            _multiCardsSelected = new List<Card>();
        }

        private void AddMultiCardSelection(Card card, GameObject visual)
        {
            if (!Open) return;

            // TODO: make the visual's code not suck (also animate)
            var selectImage = visual.transform.GetChild(0);

            if (_multiCardsSelected.Contains(card))
            {
                _multiCardsSelected.Remove(card);
                selectImage.gameObject.SetActive(false);
            }
            else
            {
                _multiCardsSelected.Add(card);
                selectImage.gameObject.SetActive(true);
            }

            confirmButton.interactable = _multiCardsSelected.Count == _currentRequest.MultiCardExcpectedAmount;
        }

        private void SelectSingleCard(Card card)
        {
            if (!Open) return;
            
            if (!_currentRequest.IsCardEligible(card))
            {
                header.text = "Invalid card!";
                return;
            }

            _singleCardSelection = card;

            confirmButton.interactable = true;

        }
        
        public void Cancel()
        {
            DeclareDone();
        }

        public void Confirm()
        {
            switch (_currentRequest.InpType)
            {
                case InputType.SingleCardTarget:
                    _currentRequest.CardInput = _singleCardSelection;
                    break;
                case InputType.MultiCardTarget:
                    _currentRequest.MultiCardInput = _multiCardsSelected;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            DeclareDone();
        }
    }
}