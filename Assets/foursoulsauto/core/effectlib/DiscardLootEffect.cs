using System;
using System.Collections;
using foursoulsauto.core.deck;
using foursoulsauto.core.player;
using UnityEngine;

namespace foursoulsauto.core.effectlib
{
    public class DiscardLootEffect : IStackEffect
    {
        private readonly Player _target;
        private readonly Func<int> _funcAmount;

        public DiscardLootEffect(Player target, Func<int> funcAmount)
        {
            _target = target;
            _funcAmount = funcAmount;
        }

        public IEnumerator Resolve()
        {
            var amount = _funcAmount.Invoke();
            if (amount <= 0) yield break;
            
            var request = new EffectInput(card => card.Container == _target.Hand, amount);
            _target.RequestInput(request);
            yield return new WaitUntil(() => request.Filled);
            Board.Instance.Discard(request.MultiCardInput, Deck.Loot);
        }

        public string GetEffectText()
        {
            return $"{_target.CharName} will discard {_funcAmount.Invoke()} loot cards";
        }
    }
}