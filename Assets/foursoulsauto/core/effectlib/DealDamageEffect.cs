using System.Collections.Generic;
using System.Linq;

namespace foursoulsauto.core.effectlib
{
    public class DealDamageEffect : IStackEffect
    {
        private readonly Dictionary<LivingCard, int> _targetDamageDict;
        public DealDamageEffect(Dictionary<LivingCard, int> targetDamageDict)
        {
            _targetDamageDict = targetDamageDict;
        }
        
        public DealDamageEffect(List<LivingCard> targets, int value) 
        {
            _targetDamageDict = new Dictionary<LivingCard, int>();
            targets.ForEach(player => _targetDamageDict.Add(player, value));
        }
        
        public DealDamageEffect(LivingCard target, int value) 
        {
            _targetDamageDict = new Dictionary<LivingCard, int> { { target, value } };
        }
        
        public void Resolve()
        {
            foreach (var pair in _targetDamageDict)
            {
                pair.Key.TakeDamage(pair.Value);
            }
        }
        
        public string GetEffectText()
        {
            if (_targetDamageDict.Count == 1)
                return $"{_targetDamageDict.Keys.First()} takes {_targetDamageDict.Values.First()} damage";
                    
            var effectText = "";
            var targets = _targetDamageDict.Keys;
        
            for (var i = 0; i < targets.Count - 1; i++)
            {
                effectText += targets.ElementAt(i) + ", ";
            }
        
            effectText += $"and {targets.Last()} ";
        
            if (_targetDamageDict.Values.Distinct().Count() == 1) // if there is only one value
            {
                effectText += $"take {_targetDamageDict.Values.First()} damage";
            }
            else
            {
                effectText += "take ";
                for (var i = 0; i < targets.Count - 1; i++)
                {
                    effectText += _targetDamageDict.Values.ElementAt(i) + ", ";
                }
        
                effectText += $"and {_targetDamageDict.Values.Last()} damage, respectively";
            }
        
            return effectText;
        }
    }
}