using System.Collections.Generic;
using foursoulsauto.core;
using foursoulsauto.core.player;
using TMPro;
using UnityEngine;

namespace foursoulsauto.ui
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Player player;
        [SerializeField] private TMP_Text playerCentText;
        [SerializeField] private Transform stackPanel;
        [SerializeField] private TMP_Text headerText;
        [SerializeField] private TMP_Text passText;
        
        private UIStackHandler _stackHandler; // TODO: almost definitely should be its own MonoBehaviour

        public Player ControlledPlayer => player;
        
        public Card GetCardUnderMouse()
        {
            var pos = playerCamera.ScreenToWorldPoint(Input.mousePosition);
                    
            if (!Physics.Raycast(
                    pos, Vector3.forward, out var hit,
                    Mathf.Infinity, LayerMask.GetMask("Card"))) return null;
        
            return hit.transform.GetComponentInParent<Card>();
        }

        public void OpenHeader(string msg) // TODO: animations and colors and shit
        {
            headerText.gameObject.SetActive(true);
            headerText.text = msg;
        }

        public void CloseHeader() // TODO: same as OpenHeader
        {
            headerText.gameObject.SetActive(false);
        }
        
        private void Awake()
        {
            player.CentsChanged += PlayerOnCentsChanged;
            player.GainedPriority += OnGainPriority;
        }
        private void UpdatePassText() => 
            passText.text = Board.Instance.Stack.IsEmpty ? Board.Instance.Phase.EmptyStackPassText : "Pass";

        private void OnGainPriority()
        {
            UpdatePassText();
        }

        private void Start()
        {
            _stackHandler = new UIStackHandler(this);
        }

        private void PlayerOnCentsChanged()
        {
            playerCentText.text = player.Cents + "¢"; 
        }

        public void PlayerPass()
        {
            player.Pass();
            UpdatePassText();
        }

        private class UIStackHandler
        {
            private readonly PlayerUI _ui;
            private Dictionary<IVisualStackEffect, GameObject> _effectToGameobject = new();
            public UIStackHandler(PlayerUI ui)
            {
                _ui = ui;

                var stack = Board.Instance.Stack;//TODO: Animate
                stack.ItemPushed += OnItemPushed;
                stack.ItemFizzled += OnItemFizzled;
                stack.ItemResolved += OnItemFizzled; //TODO: make fizzled and resolved different (cool disintegration effect)
            }
            
            private void OnItemFizzled(IVisualStackEffect obj)
            {
                var stackMember = _effectToGameobject[obj];
                _effectToGameobject.Remove(obj);
                Destroy(stackMember);
            }

            private void OnItemPushed(IVisualStackEffect obj)
            {
                var stackMember = obj.CreateStackVisual();
                stackMember.transform.SetParent(_ui.stackPanel);
                _effectToGameobject.Add(obj, stackMember);
            }
        }

        public void GenerateEffect(CardAction action)
        {
            player.PlayEffect(action);
            UpdatePassText();
        }
    }
}
