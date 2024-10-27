using System;
using System.Collections.Generic;
using foursoulsauto.core.player;

namespace foursoulsauto.core.effectlib
{
    public abstract class AbstractPlayersGainEffect : GenericTargetsDoVerbEffect<Player>
    {
        protected AbstractPlayersGainEffect(Dictionary<Player, Func<int>> targetToAmountDict) : base(targetToAmountDict)
        {
        }

        protected AbstractPlayersGainEffect(Player target, Func<int> amount) : base(target, amount)
        {
        }

        protected AbstractPlayersGainEffect(List<Player> targets, Func<int> amount) : base(targets, amount)
        {
        }

        protected override string VerbString => "gain";

        protected override string GetTargetName(Player target) => target.CharName;
    }
}