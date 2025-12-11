using System;
using System.Collections.Generic;
using foursoulsauto.core.effectlib;
using UnityEngine;

namespace foursoulsauto.ui
{
    public class DieRollUI : StackMemberUI
    {
        [SerializeField] private List<Sprite> sprites;

        private DieRoll _roll;

        public void Subscribe(DieRoll roll)
        {
            _roll = roll;
            _roll.ResultChanged += UpdateSprite;
            UpdateSprite(_roll.RawResult);
        }

        private void UpdateSprite(int value)
        {
            texture = sprites[value - 1].texture;
        }

        protected override void Awake()
        {
            base.Awake();
            
            if (sprites.Count == 6) return;
            
            Debug.LogError($"{this} needs exactly 6 sprites.");
            throw new Exception();
        }
    }
}