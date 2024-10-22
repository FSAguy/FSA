using System.Collections;
using foursoulsauto.core.player;
using UnityEngine;

namespace foursoulsauto.test
{
    public class CPUAutoPasser : MonoBehaviour
    {
        [SerializeField] private Player player;

        private void Awake()
        {
            player.PriorityChanged += OnPriorityChanged;
        }

        private void OnPriorityChanged()
        {
            if (player.HasPriority)
            {
                Debug.Log("UHHHHH I THINK ILL PASS");
                Invoke(nameof(Pass), 1f);
            }
        }

        private void Pass()
        {
            player.Pass();
        }
    }
}