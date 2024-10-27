using System.Collections.Generic;
using foursoulsauto.core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foursoulsauto.ui.player
{
    
    public class CardActionUI : PlayerUIModule
    {
        private enum State { Idle, Generating}

        [SerializeField] private GameObject visuals;
        [SerializeField] private GameObject actionPanel;
        [SerializeField] private TMP_Text cardTitle;
        [SerializeField] private Button buttonClone;

        private State _state = State.Idle;
        private List<Button> _buttons = new();
        private CardAction _currentAction;

        protected override void OnClose()
        {
            CloseVisuals();
        }

        protected override void OnOpen()
        {
            // uhhhh
        }

        protected override void Start()
        {
            base.Start();
            CloseVisuals();
        }

        public void Cancel()
        {
            _state = State.Idle;
            CloseVisuals();
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
            if (!Open) return; // TODO: still a bit wonky...
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
            if (!Open) return;
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
                Cancel();
                return;
            }
            
            if (action.Input.InpType == InputType.None)
            {
                Manager.GenerateEffect(action);
                CloseVisuals();
                DeclareDone();
                return;
            }
            
            CloseVisuals();
            _currentAction = action;
            _state = State.Generating;
            Manager.RequestInput(_currentAction.Input);
        }
        
        
        // TODO: give more power to DefaultPlayerUI?
        // specifically instead of directly telling the manager to play action,
        // maybe keep action as public, declare done, and make defaultui handle it?
        // TODO: some effect are cancelable, connect EffectInputUI to this so EffectInputUI could tell if it cancelled or not
        private void GeneratingActionUpdate()
        {
            if (!_currentAction.Input.Filled) return;
            Manager.GenerateEffect(_currentAction);
            DeclareDone();
            _state = State.Idle;
            CloseVisuals();
        }   
    }
}