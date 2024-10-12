using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace foursoulsauto.ui
{
    public class DieRollUI : MonoBehaviour
    {
        [SerializeField] private List<Sprite> sprites;
        [SerializeField] private Image dieRenderer;
        
        public void UpdateSprite(int value)
        {
            dieRenderer.sprite = sprites[value - 1];
        }

        private void Awake()
        {
            if (sprites.Count != 6)
            {
                Debug.LogError($"{this} needs exactly 6 sprites.");
                throw new Exception();
            }
        }
    }
}