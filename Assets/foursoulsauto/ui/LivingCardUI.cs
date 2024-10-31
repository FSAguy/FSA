using System;
using foursoulsauto.core;
using TMPro;
using UnityEngine;

namespace foursoulsauto.ui
{
    public class LivingCardUI : CardUI
    {
        [SerializeField] private GameObject statPanel;
        [SerializeField] private TMP_Text hpText;
        [SerializeField] private TMP_Text evasionText;
        [SerializeField] private TMP_Text attackText;

        // TODO: react to evasion and attack, make them optional, colors and animations and shid
        protected override void Awake()
        {
            base.Awake();
            if (TargetCard is not LivingCard livingCard)
            {
                Debug.LogError($"{TargetCard.name} is not LivingCard");
                throw new Exception();
            }

            livingCard.HpChanged += OnHpChanged;
            livingCard.AttackChanged += OnAttackChanged;
            livingCard.EvasionChanged += OnEvasionChanged;
        }

        private void OnAttackChanged(int attack)
        {
            attackText.text = attack.ToString();
        }

        private void OnEvasionChanged(int evasion)
        {
            if (evasionText is not null) // TODO: should probably always have evasion and just hide it when unneeded
                evasionText.text = evasion + (evasion == 6 ? "" : "+");
        }

        protected override void OnChangedFace(bool isUp)
        {
            base.OnChangedFace(isUp);
            statPanel.SetActive(isUp);
        }

        private void OnHpChanged(int hp)
        {
            Debug.Log($"HP CHANGED: {TargetCard.CardName}");
            hpText.text = hp.ToString();
        }
    }
}