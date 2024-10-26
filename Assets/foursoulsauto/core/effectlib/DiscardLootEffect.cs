using System;
using System.Collections;
using foursoulsauto.core.player;
using UnityEngine;

namespace foursoulsauto.core.effectlib
{
    public class DiscardLootEffect : IStackEffect
    {
        private Player _target;
        private Func<int> _funcAmount;

        public IEnumerator Resolve()
        {
            var request = new EffectInput(card => card.Container == _target.Hand, _funcAmount.Invoke());
            _target.RequestInput(request);
            yield return new WaitUntil(() => request.Filled);
            request.MultiCardInput.ForEach(card => card.Discard());
        }

        public string GetEffectText()
        {
            return $"{_target.CharName} will discard {_funcAmount.Invoke()} loot cards";
        }
    }
}