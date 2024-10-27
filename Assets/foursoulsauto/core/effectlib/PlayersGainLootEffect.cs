using System;
using System.Collections;
using System.Collections.Generic;
using foursoulsauto.core.player;

namespace foursoulsauto.core.effectlib
{
    public class PlayersGainLootEffect : AbstractPlayersGainEffect
    {
        public PlayersGainLootEffect(Dictionary<Player, Func<int>> targetToAmountDict) : base(targetToAmountDict)
        {
        }

        public PlayersGainLootEffect(Player target, Func<int> amount) : base(target, amount)
        {
        }

        public PlayersGainLootEffect(List<Player> targets, Func<int> amount) : base(targets, amount)
        {
        }

        protected override string UnitTypeString => "loot";
        
        public override IEnumerator Resolve()
        {
            // TODO: must be done IN PLAYER ORDER - active player first!
            foreach (var (key, value) in TargetToAmountDict)
            {
                Board.Instance.PlayerLoot(key, value.Invoke());
            }

            yield break;
        }
    }
}

