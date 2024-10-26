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
        
        private EffectInput _currentRequest;

        protected override void OnClose()
        {
            inputScreen.gameObject.SetActive(false);
        }

        protected override void OnOpen()
        {
            inputScreen.gameObject.SetActive(true);
        }

        protected override void Start()
        {
            base.Start();
            Manager.CardClicked += (card, _) => SelectCard(card);
        }

        public void GetPlayerInput(EffectInput request)
        {
            _currentRequest = request;
            inputScreen.gameObject.SetActive(true);
            header.text = "Select Target";
        }

        private void SelectCard(Card card)
        {
            if (!Open) return;
            
            if (!_currentRequest.IsCardEligible(card))
            {
                header.text = "Invalid card!";
                return;
            }
            
            _currentRequest.CardInput = card;
            
            DeclareDone();
        }
    }
}