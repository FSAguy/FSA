using System;
using UnityEngine;

namespace Core
{
    public class Player : MonoBehaviour
    {
        public event Action PlayerPassed;
    
        public void Pass()
        {
            PlayerPassed?.Invoke();
        }
    
    }
}
