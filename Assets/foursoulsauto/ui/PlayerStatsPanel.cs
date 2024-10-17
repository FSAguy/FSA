using TMPro;
using UnityEngine;

namespace foursoulsauto.ui
{
    public class PlayerStatsPanel : MonoBehaviour
    {
        [SerializeField] private PlayerUI ui;
        [SerializeField] private TMP_Text centText;
        [SerializeField] private TMP_Text lootPlaysText;
        [SerializeField] private TMP_Text attacksText;

        private void Awake()
        {
            var player = ui.ControlledPlayer;
            player.AttacksLeftChanged += RedrawStats;
            player.LootPlaysChanged += RedrawStats;
            player.CentsChanged += RedrawStats;
        }

        private void RedrawStats()
        {
            attacksText.text = "Attacks:" + ui.ControlledPlayer.AttacksRemaining;
            lootPlaysText.text = "Loot:" + ui.ControlledPlayer.LootPlaysRemaining;
            centText.text = ui.ControlledPlayer.Cents + "¢"; 
        }
    }
}