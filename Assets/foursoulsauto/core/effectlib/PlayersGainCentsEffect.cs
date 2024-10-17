using System;
using System.Collections.Generic;
using System.Linq;
using foursoulsauto.core.player;

namespace foursoulsauto.core.effectlib
{
    // TODO: either make effect include losing cents as negative gain, or make a new effect to handle it
    public class PlayersGainCentsEffect : IStackEffect
    {
        private readonly Dictionary<Player, Func<int>> _gains;
        public PlayersGainCentsEffect(Dictionary<Player, Func<int>> gains) 
        {
            _gains = gains;
        }

        public PlayersGainCentsEffect(List<Player> players, Func<int> value) 
        {
            _gains = new Dictionary<Player, Func<int>>();
            players.ForEach(player => _gains.Add(player, value));
        }

        public PlayersGainCentsEffect(Player player, Func<int> value) 
        {
            _gains = new Dictionary<Player, Func<int>> { { player, value } };
        }
        public void Resolve()
        {
            foreach (var pair in _gains)
            {
                pair.Key.Cents += pair.Value.Invoke();
            }
        }

        public string GetEffectText()
        {
            if (_gains.Count == 1)
                return $"{_gains.Keys.First()} will gain {_gains.Values.First().Invoke()}¢";
            
            var effectText = "";
            var players = _gains.Keys;

            for (var i = 0; i < players.Count - 1; i++)
            {
                effectText += players.ElementAt(i) + ", ";
            }

            effectText += $"and {players.Last()} ";

            var first = _gains.Values.First().Invoke();
            if (_gains.Values.All(func => func.Invoke() == first)) // if there is only one value
            {
                effectText += $"gain {first}¢";
            }
            else
            {
                effectText += "gain ";
                for (var i = 0; i < players.Count - 1; i++)
                {
                    effectText += _gains.Values.ElementAt(i).Invoke() + "¢, ";
                }

                effectText += $"and {_gains.Values.Last().Invoke()}¢, respectively";
            }

            return effectText;
        }
    }
}