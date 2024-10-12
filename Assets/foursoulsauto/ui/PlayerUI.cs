using foursoulsauto.core;
using foursoulsauto.core.player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foursoulsauto.ui
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Player player;
        [SerializeField] private TMP_Text centText;
        [SerializeField] private Transform stackPanel;
        [SerializeField] private Button backgroundCancelButton;
        [SerializeField] private CardActionUI actionUI;
        
        private UICardPicker _picker;
        private UIStackHandler _stackHandler;

        private void Awake()
        {
            player.CentsChanged += PlayerOnCentsChanged;
            backgroundCancelButton.onClick.AddListener(SetDefaultUI);
            SetDefaultUI();
        }

        private void Start()
        {
            _picker = new UICardPicker(this);
            _stackHandler = new UIStackHandler(this);
        }

        private void PlayerOnCentsChanged()
        {
            centText.text = player.Cents + "¢"; 
        }

        public void PlayerPass()
        {
            player.Pass();
        }

        private void Update()
        {
            if (!Input.GetMouseButtonDown(0)) return;
            if (actionUI.IsOpen) return;
            var card = _picker.PickCard();
            if (card is null || (card.Container.Owner != player && card.Container.Owner is not null)) 
                SetDefaultUI();
            else
            {
                OpenCancelButton();
                actionUI.Open(Input.mousePosition, card);
            }
        }

        private void OpenCancelButton()
        {
            backgroundCancelButton.gameObject.SetActive(true);
        }

        private void SetDefaultUI()
        {
            actionUI.Close();
            backgroundCancelButton.gameObject.SetActive(false);
        }

        private class UIStackHandler
        {
            private readonly PlayerUI _ui;
            public UIStackHandler(PlayerUI ui)
            {
                _ui = ui;
                
                var stack = Board.Instance.Stack;
                stack.ItemPushed += OnItemPushed;
                stack.ItemFizzled += OnItemFizzled;
                stack.ItemResolved += OnItemFizzled; //TODO: Animate
            }
            private void OnItemFizzled(IVisualStackEffect obj)
            {
                // todo: maybe introduce a mapping between objects instead of removing last always?
                // though it is a STACK so probably ok
                var amount = _ui.stackPanel.childCount;
                Destroy(_ui.stackPanel.GetChild(amount - 1).gameObject);
            }

            private void OnItemPushed(IVisualStackEffect obj)
            {
                var stackMember = obj.GetStackVisual();
                stackMember.transform.SetParent(_ui.stackPanel);
            }
        }
        private class UICardPicker
        {
            private readonly PlayerUI _ui;
            public UICardPicker(PlayerUI ui)
            {
                _ui = ui;
            }
            public Card PickCard()
            {
                var pos = _ui.playerCamera.ScreenToWorldPoint(Input.mousePosition);
            
                if (!Physics.Raycast(
                        pos, Vector3.forward, out var hit,
                        Mathf.Infinity, LayerMask.GetMask("Card"))) return null;

                return hit.transform.GetComponentInParent<Card>();
            }
        }

        public void PlayLoot(CardAction action)
        {
            player.PlayLoot(action);
        }
    }
}
