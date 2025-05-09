using System;
using System.Collections;

namespace foursoulsauto.core.effectlib
{
    public class AddToRollEffect : IStackEffect
    {
        private DieRoll _roll;
        private Func<int> _amount;

        public AddToRollEffect(DieRoll roll, Func<int> amount)
        {
            _roll = roll;
            _amount = amount;
        }

        public IEnumerator Resolve()
        {
            _roll.RawResult += _amount();
            yield break;
        }

        public string GetEffectText()
        {
            var start = _amount() > 0 ? "Increase" : "Decrease";
            return $"{start} a roll by {_amount}";
        }
    }
}