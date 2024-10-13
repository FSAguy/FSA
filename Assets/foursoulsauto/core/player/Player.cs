using System;
using UnityEngine;

namespace foursoulsauto.core.player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerHand hand;

        private int _cents;
        private int _lootPlaysLeft = 1; // todo: change this lol
        
        public PlayerHand Hand => hand;
        
        public event Action PlayerPassed;
        public event Action CentsChanged;

        public bool HasLootPlays => _lootPlaysLeft > 0;
        public int Cents
        {
            get => _cents;
            set
            {
                _cents = Mathf.Max(value, 0);
                CentsChanged?.Invoke();
            }
        }

        public void Pass()
        {
            PlayerPassed?.Invoke();
        }

        public void PlayLoot(CardAction action)
        {
            _lootPlaysLeft--;
            Board.Instance.PlayEffect(action.GenerateEffect());
        }
        
    }
}
