using TMPro;
using UnityEngine;

namespace Core
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Player player;
        [SerializeField] private TMP_Text centText;
        [SerializeField] private Transform panel;
        private UICardPicker _picker;

        private void Awake()
        {
            player.CentsChanged += PlayerOnCentsChanged;
        }

        private void Start()
        {
            _picker = new UICardPicker(this);
            var stack = Board.Instance.Stack;
            stack.ItemPushed += StackOnItemPushed;
        }

        private void StackOnItemPushed(IStackEffect obj)
        {
            var stackMember = obj.GetStackVisual();
            stackMember.transform.SetParent(panel);
        }

        private void PlayerOnCentsChanged()
        {
            centText.text = player.Cents + "$"; // TODO: change to ¢
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
