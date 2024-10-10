using System;
using UnityEngine;

namespace core.effectlib
{
    public class RollCreatingEffect : IStackEffect
    {
        private bool _rollCertain;
        private int _rollFinal;
        private readonly DieRoll _roll;
        private readonly IStackEffect[] _potentialEffects;
        
        public int Result => _rollCertain ? _rollFinal : _roll.Result;

        public RollCreatingEffect(DieRoll roll, IStackEffect[] potentialEffects)
        {
            _roll = roll;
            _roll.RollResolved += FinalizeRoll;
            _potentialEffects = potentialEffects;
            if (_potentialEffects.Length != 6)
            {
                Debug.LogError($"{this} Must have 6 potential effects!");
                throw new Exception();
            }  
        }

        public void FinalizeRoll(int rollResult)
        {
            _rollFinal = rollResult;
            _rollCertain = true;
        }

        public void OnStackAdd()
        {
            Board.Instance.Stack.Push(_roll);
        }

        public void Resolve()
        {
            _potentialEffects[Result - 1].Resolve();
        }

        public string GetEffectText()
        {
            return _potentialEffects[Result - 1].GetEffectText();
        }
    }
}