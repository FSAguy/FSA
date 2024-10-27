using System;
using System.Collections.Generic;
using foursoulsauto.core.effectlib;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace foursoulsauto.ui
{
    public class DieRollUI : StackMemberUI
    {
        [SerializeField] private List<Sprite> sprites;
        [SerializeField] private Image dieRenderer;

        private DieRoll _roll;

        public void Subscribe(DieRoll roll)
        {
            _roll = roll;
            _roll.ResultChanged += UpdateSprite;
            UpdateSprite(_roll.RawResult);
        }

        private void UpdateSprite(int value)
        {
            dieRenderer.sprite = sprites[value - 1];
        }

        private void Awake()
        {
            if (sprites.Count == 6) return;
            
            Debug.LogError($"{this} needs exactly 6 sprites.");
            throw new Exception();
        }
    }
}