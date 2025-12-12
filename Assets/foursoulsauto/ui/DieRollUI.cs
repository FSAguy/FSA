using System;
using System.Collections.Generic;
using foursoulsauto.core.effectlib;
using UnityEngine;

namespace foursoulsauto.ui
{
    public class DieRollUI : StackMemberUI
    {
        [SerializeField] private List<Texture> textures;

        private DieRoll _roll;

        public void Subscribe(DieRoll roll)
        {
            _roll = roll;
            _roll.ResultChanged += UpdateSprite;
            UpdateSprite(_roll.RawResult);
        }

        private void UpdateSprite(int value)
        {
            texture = textures[value - 1];
        }

        protected override void Awake()
        {
            base.Awake();
            
            if (textures.Count == 6) return;
            
            Debug.LogError($"{this} needs exactly 6 textures.");
            throw new Exception();
        }
    }
}