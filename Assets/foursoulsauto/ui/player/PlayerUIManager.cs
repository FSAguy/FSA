using foursoulsauto.core;
using foursoulsauto.core.player;
using UnityEngine;
using UnityEngine.EventSystems;

namespace foursoulsauto.ui.player
{
    // TODO: reduce annoying state management if possible
    public class PlayerUIManager : MonoBehaviour
    {
        private enum State {Default, ActionSelect, EffectInput}
        
        [SerializeField] private DefaultPlayerUI defaultUI;
        [SerializeField] private CardActionUI actionUI;
        [SerializeField] private EffectInputUI effectUI;
        [SerializeField] private Player player;

        public Player ControlledPlayer => player;

        private State _state;

        private void Start()
        {
            foreach (var card in Board.Instance.AllCards)
            {
                var cardUI = card.GetComponentInChildren<CardUI>();
                cardUI.CardClicked += OnCardClicked;
            }
            actionUI.Stopped += OnActionUIStopped;
            effectUI.Finished += OnEffectUIFinished;

            player.RequestedInput += RequestInput;
            _state = State.Default;
        }

        private void OnEffectUIFinished()
        {
            defaultUI.Show();
            effectUI.Hide();
            _state = State.Default;
        }

        private void OnActionUIStopped()
        {
            _state = State.Default;
        }

        private void OnCardClicked(Card card, PointerEventData pointerData)
        {
            switch (_state)
            {
                case State.Default:
                    actionUI.SelectAction(card, pointerData.position);
                    _state = State.ActionSelect;
                    break;
                case State.EffectInput:
                    effectUI.SelectCard(card);
                    break;
            }
        }
        
        public void GenerateEffect(CardAction action)
        {
            player.PlayEffect(action);
            effectUI.Hide();
            defaultUI.Show();
            _state = State.Default;
        }

        public void RequestInput(EffectInput request)
        {
            defaultUI.Hide();
            effectUI.Show();
            effectUI.GetPlayerInput(request);
            _state = State.EffectInput;
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