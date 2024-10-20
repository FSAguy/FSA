using System;
using System.Collections;

namespace foursoulsauto.core.effectlib
{
    public class RerollEffect : IStackEffect
    {
        private DieRoll _target;

        public RerollEffect(DieRoll target)
        {
            _target = target;
        }

        public IEnumerator Resolve()
        {
            _target.ReRoll();
            yield return null;
        }

        public string GetEffectText()
        {
            return $"Reroll the {_target.Result}";
        }
    }
}