using foursoulsauto.core;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace foursoulsauto.ui.player
{
    public class DefaultPlayerUI : PlayerUIModule
    {
        // TODO: make actionUI open only with priority
        [SerializeField] private CardActionUI actionUI;
        [SerializeField] private GameObject defaultUiPanel;
        [SerializeField] private TMP_Text passText;
        [SerializeField] private Button attackBtn;
        [SerializeField] private Button passBtn;
        [SerializeField] private PlayerStatsPanel statsPanel;

        private void UpdateVisuals()
        {
            passText.text = Board.Instance.Stack.IsEmpty ? Board.Instance.Phase.EmptyStackPassText : "Pass";
            passBtn.interactable = Manager.ControlledPlayer.HasPriority;
            attackBtn.interactable = Manager.ControlledPlayer.MayAttack;
            statsPanel.RedrawStats();
        }

        protected override void OnOpen()
        {
            actionUI.Open = true;
            UpdateVisuals();
            defaultUiPanel.SetActive(true);
        }

        protected override void OnClose()
        {
            actionUI.Open = false;
            defaultUiPanel.SetActive(false);
        }

        protected override void Start()
        {
            base.Start();
            var player = Manager.ControlledPlayer;
            Manager.CardClicked += OnCardClicked;
            player.StateChanged += UpdateVisuals;
            // TODO: only doing this so passtext may function, should probably give that responsibility partly to stackui
            Board.Instance.Stack.ItemPushed += _ => UpdateVisuals();
        }

        private void OnCardClicked(Card card, PointerEventData pointerData)
        {
            actionUI.SelectAction(card, pointerData.position);
        }

        public void PlayerPass()
        {
            Manager.PlayerPass();
            UpdateVisuals();
        }

        public void DeclareAttack()
        {
            Manager.PlayerAttack();
            UpdateVisuals();
        }
    }
}