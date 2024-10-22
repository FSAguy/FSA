using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace foursoulsauto.core.effectlib
{
    public class DealDamageEffect : IStackEffect
    {
        private readonly Dictionary<LivingCard, Func<int>> _targetDamageDict;
        public DealDamageEffect(Dictionary<LivingCard, Func<int>> targetDamageDict)
        {
            _targetDamageDict = targetDamageDict;
        }
        
        public DealDamageEffect(List<LivingCard> targets, Func<int> value) 
        {
            _targetDamageDict = new Dictionary<LivingCard, Func<int>>();
            targets.ForEach(player => _targetDamageDict.Add(player, value));
        }
        
        public DealDamageEffect(LivingCard target, Func<int> value) 
        {
            _targetDamageDict = new Dictionary<LivingCard, Func<int>> { { target, value } };
        }

        public IEnumerator Resolve()
        {
            foreach (var pair in _targetDamageDict)
            {
                pair.Key.TakeDamage(pair.Value());
            }

            yield return null;
        }
        
        public string GetEffectText()
        {
            if (_targetDamageDict.Count == 1)
                return $"{_targetDamageDict.Keys.First().CardName} " +
                       $"takes {_targetDamageDict.Values.First().Invoke()} damage";
                    
            var effectText = "";
            var targets = _targetDamageDict.Keys;
        
            for (var i = 0; i < targets.Count - 1; i++)
            {
                var name = targets.ElementAt(i).CardName;
                effectText += name + ", ";
            }
        
            effectText += $"and {targets.Last().CardName} ";
        
            var first = _targetDamageDict.Values.First().Invoke();
            if (_targetDamageDict.Values.All(func => func.Invoke() == first)) // if there is only one value
            {
                effectText += $"take {_targetDamageDict.Values.First().Invoke()} damage";
            }
            else
            {
                effectText += "take ";
                for (var i = 0; i < targets.Count - 1; i++)
                {
                    effectText += _targetDamageDict.Values.ElementAt(i) + ", ";
                }
        
                effectText += $"and {_targetDamageDict.Values.Last().Invoke()} damage, respectively";
            }
        
            return effectText;
        }
    }
}