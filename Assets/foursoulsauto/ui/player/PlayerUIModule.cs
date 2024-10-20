using System;
using UnityEngine;

namespace foursoulsauto.ui.player
{
    public abstract class PlayerUIModule : MonoBehaviour
    {
        protected PlayerUIManager Manager;

        protected virtual void Start()
        {
            Manager = GetComponentInParent<PlayerUIManager>();
        }
    }
}