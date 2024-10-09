using core.effectlib;

namespace core.cardlib
{
    public class Fly : MonsterCard
    {
        protected override CardEffect GenerateRewards()
        {
            return new CardEffect(this, 
                new PlayersGainCentsEffect(Board.Instance.ActivePlayer, 1));
        }
    }
}