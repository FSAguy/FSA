using TMPro;
using UnityEngine;

namespace foursoulsauto.ui.player
{
    public class PlayerStatsPanel : PlayerUIModule
    {
        [SerializeField] private TMP_Text centText;
        [SerializeField] private TMP_Text lootPlaysText;
        [SerializeField] private TMP_Text attacksText;

        protected override void Start()
        {
            base.Start();
            var player = Manager.ControlledPlayer;
            player.AttacksLeftChanged += RedrawStats;
            player.LootPlaysChanged += RedrawStats;
            player.CentsChanged += RedrawStats;
        }

        public void RedrawStats()
        {
            attacksText.text = "Attacks:" + Manager.ControlledPlayer.AttacksRemaining;
            lootPlaysText.text = "Loot:" + Manager.ControlledPlayer.LootPlaysRemaining;
            centText.text = Manager.ControlledPlayer.Cents + "¢"; 
        }
    }
}