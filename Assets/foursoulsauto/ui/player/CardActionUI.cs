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
        private enum State { Idle, Selecting, Generating}

        public event Action Stopped;
        
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
        
        private void Stop()
        {
            _state = State.Idle;
            CloseActionPanel();
            Stopped?.Invoke();
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
                    break;
                case State.Selecting:
                    SelectionUpdate();
                    break;
                case State.Generating:
                    GeneratingActionUpdate();
                    break;
            }
        }

        private void SelectionUpdate()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            // disgusting hack that doesnt actually work well since you only want to cancel when player presses outside
            // only way i can think to fix it is to had a huge cancel button behind all the other ui to cancel
            // TODO: either do the above of figure out better way
            Invoke(nameof(Stop), 0.1f); 
        }
        
        public void SelectAction(Card card, Vector3 pos)
        {
            // TODO: ignore if the player does not own the card
            
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
            CancelInvoke(nameof(Stop)); // TODO: delete after removing disgusting hack above
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
            
            CloseActionPanel();
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