using foursoulsauto.core.effectlib;
using UnityEngine;

namespace foursoulsauto.core.cardlib
{
    public class Fly : MonsterCard
    {
        protected override CardEffect GenerateRewards()
        {
            return new CardEffect(this, 
                new PlayersGainCentsEffect(Board.Instance.ActivePlayer, () => 1)); // TODO: value may be manipulated?
        }
    }
}