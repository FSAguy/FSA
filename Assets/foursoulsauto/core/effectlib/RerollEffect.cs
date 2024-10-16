using System;

namespace foursoulsauto.core.effectlib
{
    public class RerollEffect : IStackEffect
    {
        private DieRoll _target;

        public RerollEffect(DieRoll target)
        {
            _target = target;
        }

        public void Resolve()
        {
            _target.ReRoll();
        }

        public string GetEffectText()
        {
            return $"Reroll the {_target.Result}";
        }
    }
}