using System;
using System.Collections.Generic;
using foursoulsauto.core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace foursoulsauto.ui
{
    public class CardActionUI : MonoBehaviour
    {
        private enum State { Idle, Selecting, Generating}
        
        [SerializeField] private GameObject actionPanel;
        [SerializeField] private TMP_Text cardTitle;
        [SerializeField] private Button buttonClone;
        [SerializeField] private PlayerUI ui;

        private State _state = State.Idle;
        private List<Button> _buttons = new();
        private CardAction _currentAction;
        public bool IsWorking => _state != State.Idle;

        private void Awake()
        {
            Stop();
        }
        
        private void Stop()
        {
            _state = State.Idle;
            ui.CloseHeader(); // TODO: make sure nothing else important is showing there
            CloseActionPanel();
        }

        private void CloseActionPanel()
        {
            actionPanel.SetActive(false);
            for (var i = 0; i < _buttons.Count; i++)
            {
                Destroy(_buttons[i].gameObject);
                _buttons.Remove(_buttons[i]);
            } 
        }
        
        private void Update()
        {
            switch (_state)
            {
                case State.Idle:
                    IdleUpdate();
                    break;
                case State.Selecting:
                    SelectionUpdate();
                    break;
                case State.Generating:
                    GeneratingActionUpdate();
                    break;
            }
        }

        private void IdleUpdate()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            var card = ui.GetCardUnderMouse();
            if (card is null || // TODO: maybe should make logical clause below less confusing or standardize it
                (card.Container.Owner != ui.ControlledPlayer && card.Container.Owner is not null)) 
                return;
            
            SelectAction(Input.mousePosition, card);
        }

        private void SelectionUpdate()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            // disgusting hack that doesnt actually work well since you only want to cancel when player presses outside
            // only way i can think to fix it is to had a huge cancel button behind all the other ui to cancel
            // TODO: either do the above of figure out better way
            Invoke(nameof(Stop), 0.1f); 
        }

        private void GeneratingActionUpdate()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            var card = ui.GetCardUnderMouse();
            if (card is null) // user cancelled by pressing outside
            {
                ui.CloseHeader();
                Stop();
                return;
            }
            if (!_currentAction.Input.IsCardEligible(card))
            {
                ui.OpenHeader("Invalid selection");
                return;
            }

            switch (_currentAction.Input.InpType)
            {
                case InputType.LivingCardTarget:
                    _currentAction.Input.LivingCardTarget = card as LivingCard;
                    break;
            }
            // TODO: selection may be multi staged (for example, destroy a card to steal another card)
            ui.GenerateEffect(_currentAction);
            Stop();
        }

        private void SelectAction(Vector3 pos, Card card)
        {
            _state = State.Selecting;
            actionPanel.transform.position = pos;
            actionPanel.SetActive(true);
            cardTitle.text = card.CardName;
            _buttons = new List<Button>();
            foreach (var action in card.Actions)
            {
                var button = Instantiate(buttonClone, actionPanel.transform);
                if (action.MayUse)
                    button.onClick.AddListener(delegate { CreateAction(action); });
                else
                    button.interactable = false;
                button.GetComponentInChildren<TMP_Text>().text = action.Text;
                _buttons.Add(button);
            }
        }

        
        private void CreateAction(CardAction action)
        {
            CancelInvoke(nameof(Stop)); // TODO: delete after removing disgusting hack
            if (!action.Input.SomeInputExists)
            {
                Stop();
                return;
            }
            
            if (action.Input.InpType == InputType.None)
            {
                ui.GenerateEffect(action);
                Stop();
                return;
            }
            
            _currentAction = action;
            _state = State.Generating;
            switch (action.Input.InpType)
            {
                case InputType.LivingCardTarget:
                    ui.OpenHeader("Select target");
                    break;
            }
        }
    }
}