using System;
using UnityEngine;

namespace foursoulsauto.core.effectlib
{
    // wrapper effect for anything involving a roll
    public class RollCreatingEffect : IStackEffect
    {
        private DieRoll _roll;
        
        public RollCreatingEffect(DieRoll roll)
        {
            _roll = roll;
        }

        public void Resolve()
        {
            Board.Instance.Stack.Push(_roll);
        }

        public string GetEffectText()
        {
            return "Will roll a die to decide what happens"; // TODO: make it actually say what the potential results are? 
        }
    }
}