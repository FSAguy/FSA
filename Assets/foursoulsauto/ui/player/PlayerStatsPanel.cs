using TMPro;
using UnityEngine;

namespace foursoulsauto.ui.player
{
    public class PlayerStatsPanel : PlayerUIModule
    {
        [SerializeField] private TMP_Text centText;
        [SerializeField] private TMP_Text lootPlaysText;
        [SerializeField] private TMP_Text attacksText;

        public void RedrawStats()
        {
            attacksText.text = "Attacks:" + Manager.ControlledPlayer.AttacksRemaining;
            lootPlaysText.text = "Loot:" + Manager.ControlledPlayer.LootPlaysRemaining;
            centText.text = Manager.ControlledPlayer.Cents + "¢"; 
        }

        protected override void OnClose()
        {
            gameObject.SetActive(false);
        }

        protected override void OnOpen()
        {
            gameObject.SetActive(true);
        }
    }
}