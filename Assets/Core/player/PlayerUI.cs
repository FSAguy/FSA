using TMPro;
using UnityEngine;

namespace core
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Player player;
        [SerializeField] private TMP_Text centText;
        [SerializeField] private Transform panel;
        private UICardPicker _picker;
        private UIStackHandler _stackHandler;

        private void Awake()
        {
            player.CentsChanged += PlayerOnCentsChanged;
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
            _picker.PickCard();
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
                var amount = _ui.panel.childCount;
                Destroy(_ui.panel.GetChild(amount - 1).gameObject);
            }

            private void OnItemPushed(IVisualStackEffect obj)
            {
                var stackMember = obj.GetStackVisual();
                stackMember.transform.SetParent(_ui.panel);
            }
        }
        private class UICardPicker
        {
            private readonly PlayerUI _ui;
            public UICardPicker(PlayerUI ui)
            {
                _ui = ui;
            }
            public void PickCard()
            {
                var pos = _ui.playerCamera.ScreenToWorldPoint(Input.mousePosition);
                if (!Physics.Raycast(
                        pos, Vector3.forward, out var hit,
                        Mathf.Infinity, LayerMask.GetMask("Card"))) return;

                var card = hit.transform.GetComponentInParent<Card>();

                if (card.Container is null || card.Container.Owner != _ui.player) return; // TODO: make it do different shid
            
                Board.Instance.PlayEffect(card.PlayAction.GenerateEffect(_ui.player));
            }
        }
    }

}
