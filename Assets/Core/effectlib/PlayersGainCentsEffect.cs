using System;
using System.Collections.Generic;
using System.Linq;

namespace core.effectlib
{
    public class PlayersGainCentsEffect : IEffect
    {
        private readonly Dictionary<Player, int> _gains;
        public PlayersGainCentsEffect(Dictionary<Player, int> gains) 
        {
            _gains = gains;
        }

        public PlayersGainCentsEffect(List<Player> players, int value) 
        {
            _gains = new Dictionary<Player, int>();
            players.ForEach(player => _gains.Add(player, value));
        }

        public PlayersGainCentsEffect(Player player, int value) 
        {
            _gains = new Dictionary<Player, int> { { player, value } };
        }
        public void Resolve()
        {
            foreach (var pair in _gains)
            {
                pair.Key.Cents += pair.Value;
            }
        }

        public string GetEffectText()
        {
            if (_gains.Count == 1)
                return $"{_gains.Keys.First()} will gain {_gains.Values.First()}¢";
            
            var effectText = "";
            var players = _gains.Keys;

            for (var i = 0; i < players.Count - 1; i++)
            {
                effectText += players.ElementAt(i) + ", ";
            }

            effectText += $"and {players.Last()} ";

            if (_gains.Values.Distinct().Count() == 1) // if there is only one value
            {
                effectText += $"gain {_gains.Values.First()}¢";
            }
            else
            {
                effectText += "gain ";
                for (var i = 0; i < players.Count - 1; i++)
                {
                    effectText += _gains.Values.ElementAt(i) + "¢, ";
                }

                effectText += $"and {_gains.Values.Last()}¢, respectively";
            }

            return effectText;
        }
    }
}