using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace foursoulsauto.core.effectlib
{
    public abstract class GenericTargetsDoVerbEffect<T> : IStackEffect
    {
        protected readonly Dictionary<T, Func<int>> TargetToAmountDict;

        protected GenericTargetsDoVerbEffect(Dictionary<T, Func<int>> targetToAmountDict)
        {
            TargetToAmountDict = targetToAmountDict;
        }

        protected GenericTargetsDoVerbEffect(T target, Func<int> amount)
        {
            TargetToAmountDict = new Dictionary<T, Func<int>> { { target, amount } };
        }

        protected GenericTargetsDoVerbEffect(List<T> targets, Func<int> amount)
        {
            TargetToAmountDict = new Dictionary<T, Func<int>>();
            targets.ForEach(target => TargetToAmountDict.Add(target, amount));
        }
        
        // remember to add space if you want a space before the string
        // the thing to gain/steal/whatever
        protected abstract string UnitTypeString { get; }
        
        protected abstract string VerbString { get; } // TODO: this might be important for things that interact with keywords like "gain"...

        protected abstract string GetTargetName(T target);

        public abstract IEnumerator Resolve();

        public string GetEffectText() 
        {
            // single target
            if (TargetToAmountDict.Count == 1)
                return $"{GetTargetName(TargetToAmountDict.Keys.First())} " +
                       $"{VerbString}s {TargetToAmountDict.Values.First().Invoke()}{UnitTypeString}";
            //
            
            // multiple targets
            var effectText = "";
            var targets = TargetToAmountDict.Keys;

            for (var i = 0; i < targets.Count - 1; i++)
            {
                effectText += GetTargetName(targets.ElementAt(i)) + ", ";
            }

            effectText += $"and {GetTargetName(targets.Last())} ";

            // single unique value
            var first = TargetToAmountDict.Values.First().Invoke();
            if (TargetToAmountDict.Values.All(func => func.Invoke() == first)) 
            {
                effectText += $"{VerbString} {first}{UnitTypeString}";
            }
            //multiple values
            else
            {
                effectText += $"{VerbString} ";
                for (var i = 0; i < targets.Count - 1; i++)
                {
                    effectText += TargetToAmountDict.Values.ElementAt(i).Invoke() + $"{UnitTypeString}, ";
                }

                effectText += $"and {TargetToAmountDict.Values.Last().Invoke()}{UnitTypeString}, respectively";
            }

            return effectText;
        }
    }
}