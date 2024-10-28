using System;
using foursoulsauto.core.effectlib;
using UnityEngine;

namespace foursoulsauto.core.cardlib
{
    public class BasicRewardsMonsterCard : MonsterCard
    {
        private enum RewardType {Cents, Loot, Treasure}

        [SerializeField] private RewardType rewardType;
        [SerializeField] private int amount;
        
        protected override CardEffect GenerateRewards()
        {
            var player = Board.Instance.ActivePlayer;
            int AmountFunc() => amount;

            IStackEffect effect = rewardType switch
            {
                RewardType.Cents => new PlayersGainCentsEffect(player, AmountFunc),
                RewardType.Loot => new PlayersGainLootEffect(player, AmountFunc),
                RewardType.Treasure => throw new Exception(), // TODO
                _ => throw new Exception() 
            };

            return new CardEffect(this, effect);
        }
    }
}