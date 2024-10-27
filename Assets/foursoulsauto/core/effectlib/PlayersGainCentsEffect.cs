using System;
using System.Collections;
using System.Collections.Generic;
using foursoulsauto.core.player;

namespace foursoulsauto.core.effectlib
{
    // TODO: either make effect include losing cents as negative gain, or make a new effect to handle it
    public class PlayersGainCentsEffect : AbstractPlayersGainEffect
    {
        protected override string UnitTypeString => "¢";

        public override IEnumerator Resolve()
        {
            foreach (var (key, value) in TargetToAmountDict)
            {
                key.Cents += value.Invoke();
            }

            yield break;
        }

        public PlayersGainCentsEffect(Dictionary<Player, Func<int>> targetToAmountDict) : base(targetToAmountDict)
        {
        }

        public PlayersGainCentsEffect(Player player, Func<int> amount) : base(player, amount)
        {
        }

        public PlayersGainCentsEffect(List<Player> players, Func<int> amount) : base(players, amount)
        {
        }
    }
}