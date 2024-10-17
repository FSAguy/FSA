using System;
using foursoulsauto.core.player;
using foursoulsauto.ui;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace foursoulsauto.core.effectlib
{
    public abstract class DieRoll : IVisualStackEffect
    {
        // TODO: make custom die graphics (very fancy)
        // TODO: add rolly roll sound
        public event Action<int> RollResolved;
        
        private static readonly DieRollUI StackMemberClone =
            Resources.Load<DieRollUI>("Prefabs/UI/RollStackMember");
        
        public Player Roller { get; private set; }
        
        private DieRollUI _stackMember;
        protected IStackEffect[] PotentialEffects;
            
        private int RawResult { get; set; }
        public int Result => Mathf.Clamp(GetResultAfterMods(RawResult), 1, 6);
        
        protected abstract int GetResultAfterMods(int roll);
        
        // Unless using some subclass overloading this, DieRoll always requires 6 effects, one for each side of the die
        protected virtual IStackEffect RolledEffect => PotentialEffects[Result - 1];

        protected DieRoll(Player roller, IStackEffect[] potentialEffects)
        {
            Roller = roller;
            PotentialEffects = potentialEffects;
        }

        public void ReRoll()
        {
            RawResult = Random.Range(1, 7);
            // TODO: kinda cringe coupling, should probably use events instead and let the UI do its thing
            // then again GetStackVisual basically demands coupling... TODO: thinkaboudit
            _stackMember.UpdateSprite(RawResult); // TODO: maybe use the final result instead? what is less confusing?
        }

        public void OnStackAdd()
        {
            ReRoll();
        }

        public void Resolve()
        {
            if (RolledEffect.MayResolve()) RolledEffect.Resolve();
            else RolledEffect.OnLeaveStack();
            RollResolved?.Invoke(Result);
        }

        public string GetEffectText() 
        {
            var text = $"Rolled a {RawResult}";
            if (RawResult != Result) text += $", effectively a {Result}";
            text += "\n" + RolledEffect.GetEffectText();
            return text;
        }

        public GameObject CreateStackVisual() 
        {
            var stackMember = Object.Instantiate(StackMemberClone.gameObject);
            _stackMember = stackMember.GetComponent<DieRollUI>();
            
            return stackMember;
        }
    }
}