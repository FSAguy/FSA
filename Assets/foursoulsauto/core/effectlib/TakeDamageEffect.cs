using System;
using System.Collections;
using System.Collections.Generic;

namespace foursoulsauto.core.effectlib
{
    public class TakeDamageEffect : GenericTargetsDoVerbEffect<LivingCard>
    {
        protected override string UnitTypeString => " damage";
        protected override string VerbString => "take";

        protected override string GetTargetName(LivingCard target) => target.CardName;


        public override IEnumerator Resolve()
        {
            foreach (var (key, value) in TargetToAmountDict)
            {
                key.TakeDamage(value.Invoke());
            }

            yield break;
        }

        public TakeDamageEffect(Dictionary<LivingCard, Func<int>> targetToAmountDict) : base(targetToAmountDict)
        {
        }

        public TakeDamageEffect(LivingCard target, Func<int> amount) : base(target, amount)
        {
        }

        public TakeDamageEffect(List<LivingCard> targets, Func<int> amount) : base(targets, amount)
        {
        }
    }
}