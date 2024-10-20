using System.Collections;
using System.Collections.Generic;
using foursoulsauto.core;
using foursoulsauto.core.player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foursoulsauto.ui
{
    // TODO: maybe add a separate UI between turns? So that we don't have an "Attack" button when not active?
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Player player;
        [SerializeField] private GameObject defaultUiPanel;
        [SerializeField] private Transform stackPanel;
        [SerializeField] private TMP_Text passText;
        [SerializeField] private Button attackBtn;
        [SerializeField] private EffectInputUI effectInputUI;

        private UIStackHandler _stackHandler; // TODO: almost definitely should be its own MonoBehaviour

        public Player ControlledPlayer => player;
        
        public Card GetCardUnderMouse()
        {
            var pos = playerCamera.ScreenToWorldPoint(Input.mousePosition);
                    
            if (!Physics.Raycast(
                    pos, Vector3.forward, out var hit,
                    Mathf.Infinity, LayerMask.GetMask("Card"))
                ) return null;
        
            return hit.transform.GetComponentInParent<Card>();
        }

        public IEnumerator SelectInput(EffectInput request)
        {
            yield return effectInputUI.GetPlayerInput(request);
        }

        private void UpdateBottomPanel() // TODO: should move contents to their own appropriate modules
        // most likely playerstatspanel which would be renamed, like, basicactionspanel?
        {
            passText.text = Board.Instance.Stack.IsEmpty ? Board.Instance.Phase.EmptyStackPassText : "Pass";
            attackBtn.gameObject.SetActive(player.MayAttack);
        }

        public void Hide()
        {
            defaultUiPanel.SetActive(false);
        }

        public void Show()
        {
            defaultUiPanel.SetActive(true);
        }
        
        private void Awake()
        {
            player.CentsChanged += UpdateBottomPanel;
            player.GainedPriority += UpdateBottomPanel;
            player.AttacksLeftChanged += UpdateBottomPanel;
            player.RequestedInput += OnRequestedInput;
        }

        private void OnRequestedInput(EffectInput request)
        {
            StartCoroutine(effectInputUI.GetPlayerInput(request));
        }

        private void Start()
        {
            _stackHandler = new UIStackHandler(this);
        }

        public void PlayerPass()
        {
            player.Pass();
            UpdateBottomPanel();
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
            UpdateBottomPanel();
        }

        public void DeclareAttack()
        {
            player.DeclareAttack();
            UpdateBottomPanel();
        }
    }
}
