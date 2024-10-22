using foursoulsauto.core.player;
using UnityEngine;

namespace foursoulsauto.test
{
    public class CPUAutoPasser : MonoBehaviour
    {
        [SerializeField] private Player player;

        private void Awake()
        {
            player.GainedPriority += OnGainedPriority;
        }

        private void OnGainedPriority()
        {
            player.Pass();
        }
    }
}