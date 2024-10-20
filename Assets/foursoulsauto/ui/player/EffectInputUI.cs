using System;
using foursoulsauto.core;
using TMPro;
using UnityEngine;

namespace foursoulsauto.ui.player
{
    // TODO: more types of selections such as arranging stack order or selecting "choose one" effects
    public class EffectInputUI : PlayerUIModule
    {
        [SerializeField] private GameObject inputScreen;
        [SerializeField] private TMP_Text header;

        public event Action Finished;
        
        private EffectInput _currentRequest;

        public void GetPlayerInput(EffectInput request)
        {
            _currentRequest = request;
            inputScreen.gameObject.SetActive(true);
            header.text = "Select Target";
        }

        public void Show()
        {
            inputScreen.gameObject.SetActive(true);
        }

        public void Hide()
        {
            inputScreen.gameObject.SetActive(false);
        }

        public void SelectCard(Card card)
        {
            if (!_currentRequest.IsCardEligible(card))
            {
                header.text = "Invalid card!";
                return;
            }
            
            _currentRequest.CardInput = card;
            
            Finished?.Invoke();
        }
    }
}