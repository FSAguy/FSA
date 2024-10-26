using System;
using UnityEngine;

namespace foursoulsauto.ui.player
{
    public abstract class PlayerUIModule : MonoBehaviour
    {
        public event Action Done;
        private bool _open;

        public bool Open
        {
            get => _open;
            set
            {
                if (value == _open) return;
                _open = value;
                if (_open) OnOpen(); else OnClose();
            }
        }

        protected void DeclareDone() => Done?.Invoke();

        protected abstract void OnClose();
        protected abstract void OnOpen();

        protected PlayerUIManager Manager;

        protected virtual void Start()
        {
            Manager = GetComponentInParent<PlayerUIManager>();
        }
    }
}