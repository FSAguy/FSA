using System.Collections;
using foursoulsauto.core;
using TMPro;
using UnityEngine;

namespace foursoulsauto.ui
{
    public class EffectInputUI : MonoBehaviour
    {
        [SerializeField] private PlayerUI defaultUI;
        [SerializeField] private GameObject inputScreen;
        [SerializeField] private TMP_Text header;
        
        private EffectInput _currentRequest;

        public bool IsDone => _currentRequest.IsInputFilled;

        public IEnumerator GetPlayerInput(EffectInput request)
        {
            _currentRequest = request;
            inputScreen.gameObject.SetActive(true);
            defaultUI.Hide();
            header.text = "Select Target";
            yield return AwaitInput();
        }

        private IEnumerator AwaitInput()
        {
            while (!_currentRequest.IsInputFilled)
            {
                while (!Input.GetMouseButtonDown(0)) yield return null;
                var card = defaultUI.GetCardUnderMouse();
                if (card is null) // user cancelled by pressing outside TODO: Check if can cancel, then do
                {
                    continue;
                }    
                
                switch (_currentRequest.InpType)
                {
                    case InputType.SingleCardTarget:
                        if (!_currentRequest.IsCardEligible(card))
                        {
                            header.text = "Invalid card!";
                            continue;
                        }
                        _currentRequest.CardInput = card;
                        continue;
                }    
            }
            
            // TODO: add more selection types
            
            defaultUI.Show();
            inputScreen.gameObject.SetActive(false);
        }
    }
}