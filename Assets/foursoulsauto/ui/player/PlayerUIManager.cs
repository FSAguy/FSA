using System;
using System.Collections.Generic;
using foursoulsauto.core;
using foursoulsauto.core.player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace foursoulsauto.ui.player
{
    public class PlayerUIManager : MonoBehaviour
    {
        [SerializeField] private DefaultPlayerUI defaultUI;
        [SerializeField] private EffectInputUI effectUI;
        [SerializeField] private Player player;

        private List<PlayerUIModule> _modules;

        public Player ControlledPlayer => player;

        public event Action<Card, PointerEventData> CardClicked;

        private void Awake()
        {
            _modules = new List<PlayerUIModule> { defaultUI, effectUI };
        }

        private void Start()
        {
            foreach (var card in Board.Instance.AllCards)
            {
                var cardUI = card.GetComponentInChildren<CardUI>();
                cardUI.CardClicked += OnCardClicked;
            }
            player.RequestedInput += RequestInput;
            
            _modules.ForEach(module => module.Done += SetDefault);
            SetDefault();
        }

        private void SetDefault()
        {
            _modules.ForEach(module => module.Open = module == defaultUI);
        }

        private void ShowOnly(PlayerUIModule toOpen)
        {
            _modules.ForEach(module => module.Open = false);
            toOpen.Open = true;
        }

        private void OnCardClicked(Card card, PointerEventData pointerData)
        {
            CardClicked?.Invoke(card, pointerData);
        }
        
        public void GenerateEffect(CardAction action)
        {
            player.PlayEffect(action);
        }

        public void RequestInput(EffectInput request)
        {
            ShowOnly(effectUI);
            effectUI.GetPlayerInput(request);
        }

        public void PlayerPass()
        {
            player.Pass();
        }

        public void PlayerAttack()
        {
            player.DeclareAttack();
        }
    }
}