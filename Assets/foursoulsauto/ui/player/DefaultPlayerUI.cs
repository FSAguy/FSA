using foursoulsauto.core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace foursoulsauto.ui.player
{
    // TODO: maybe add a separate UI between turns? So that we don't have an "Attack" button when not active?
    public class DefaultPlayerUI : PlayerUIModule
    {
        [SerializeField] private GameObject defaultUiPanel;
        [SerializeField] private TMP_Text passText;
        [SerializeField] private Button attackBtn;
        [SerializeField] private StackUI stackUI;
        [SerializeField] private PlayerStatsPanel statsPanel;

        public void UpdateVisuals() 
        {
            passText.text = Board.Instance.Stack.IsEmpty ? Board.Instance.Phase.EmptyStackPassText : "Pass";
            attackBtn.gameObject.SetActive(Manager.ControlledPlayer.MayAttack);
            statsPanel.RedrawStats();
        }

        public void Hide()
        {
            defaultUiPanel.SetActive(false);
        }

        public void Show()
        {
            UpdateVisuals();
            defaultUiPanel.SetActive(true);
        }

        protected override void Start()
        {
            base.Start();
            var player = Manager.ControlledPlayer;
            player.CentsChanged += UpdateVisuals;
            player.GainedPriority += UpdateVisuals;
            player.AttacksLeftChanged += UpdateVisuals;
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