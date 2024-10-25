using System;
using System.Collections.Generic;
using foursoulsauto.core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foursoulsauto.ui.player
{
    // TODO: make less dumb maybe?
    public class CardActionUI : PlayerUIModule
    {
        private enum State { Idle, Generating}

        public event Action Stopped;

        [SerializeField] private GameObject visuals;
        [SerializeField] private GameObject actionPanel;
        [SerializeField] private TMP_Text cardTitle;
        [SerializeField] private Button buttonClone;

        private State _state = State.Idle;
        private List<Button> _buttons = new();
        private CardAction _currentAction;
        
        protected override void Start()
        {
            base.Start();
            Stop();
        }
        
        public void Stop()
        {
            _state = State.Idle;
            CloseVisuals();
            Stopped?.Invoke();
        }

        private void CloseVisuals()
        {
            visuals.SetActive(false);
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
                    break;
                case State.Generating:
                    GeneratingActionUpdate();
                    break;
            }
        }

        public void SelectAction(Card card, Vector3 pos)
        {
            // TODO: ignore if the player does not own the card
            if (card.Owner != Manager.ControlledPlayer) return;
            
            visuals.SetActive(true);
            actionPanel.transform.position = pos;
                                   
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
            if (!action.Input.SomeInputExists)
            {
                Stop();
                return;
            }
            
            if (action.Input.InpType == InputType.None)
            {
                Manager.GenerateEffect(action);
                Stop();
                return;
            }
            
            CloseVisuals();
            _currentAction = action;
            _state = State.Generating;
            Manager.RequestInput(_currentAction.Input);
        }
        
        private void GeneratingActionUpdate()
        {
            if (!_currentAction.Input.IsInputFilled) return;
            Manager.GenerateEffect(_currentAction);
            Stop();
        }   
    }
}