using System;

namespace foursoulsauto.core.effectlib
{
    // wrapper effect for conditional effect resolutions
    public class IfThenEffect : IStackEffect
    {
        private readonly Func<bool> _condition;
        private readonly string _conditionText;
        private readonly IStackEffect _thenEffect;
        private readonly IStackEffect _elseEffect;

        public IfThenEffect(Func<bool> condition, string conditionText, 
            IStackEffect thenEffect, IStackEffect elseEffect = null)
        {
            _condition = condition;
            _conditionText = conditionText;
            _thenEffect = thenEffect;
            _elseEffect = elseEffect;
        }

        public void Resolve()
        {
            if (_condition()) _thenEffect.Resolve();
            else _elseEffect?.Resolve();
        }

        public string GetEffectText()
        {
            var text = $"if {_conditionText}, then {_thenEffect.GetEffectText()}";
            if (_elseEffect is not null) text += $", otherwise {_elseEffect.GetEffectText()}";
            return text;
        }
    }
}