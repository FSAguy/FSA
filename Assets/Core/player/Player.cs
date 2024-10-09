using System;
using UnityEngine;

namespace core
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerHand hand;

        public PlayerHand Hand => hand;
        private int _cents;
        public event Action PlayerPassed;

        public event Action CentsChanged;
        public int Cents
        {
            get => _cents;
            set
            {
                _cents = value;
                CentsChanged?.Invoke();
            }
        }

        public void Pass()
        {
            PlayerPassed?.Invoke();
        }
    }
}
