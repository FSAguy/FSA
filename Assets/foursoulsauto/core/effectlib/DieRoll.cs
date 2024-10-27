using System;
using System.Collections;
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
        public event Action<int> ResultChanged;
        
        // TODO: doing this is bad and not good
        // Should probably make a library for these things
        private static readonly DieRollUI StackMemberClone =
            Resources.Load<DieRollUI>("Prefabs/UI/RollStackMember");
        
        public Player Roller { get; private set; }
        
        protected IStackEffect[] PotentialEffects;
        private int _rawResult;

        public int RawResult
        {
            get => _rawResult;
            set
            {
                var prev = _rawResult;
                _rawResult = value;
                if (prev != _rawResult) ResultChanged?.Invoke(_rawResult);
            }
        } 
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
        }

        public void OnStackAdd()
        {
            ReRoll();
        }

        public IEnumerator Resolve()
        {
            if (RolledEffect.MayResolve()) yield return RolledEffect.Resolve();
            RollResolved?.Invoke(Result);
            yield return null;
        }

        public void OnLeaveStack()
        {
            RolledEffect.OnLeaveStack();
        }

        public string GetEffectText() 
        {
            var text = $"Rolled a {RawResult}";
            if (RawResult != Result) text += $", effectively a {Result}";
            text += "\n" + RolledEffect.GetEffectText();
            return text;
        }

        public StackMemberUI CreateStackVisual() 
        {
            var stackMember = Object.Instantiate(StackMemberClone);
            stackMember.Subscribe(this);
            
            return stackMember;
        }
    }
}