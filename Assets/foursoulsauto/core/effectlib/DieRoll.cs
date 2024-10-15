using System;
using foursoulsauto.core.player;
using foursoulsauto.ui;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace foursoulsauto.core
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
        private IStackEffect[] _potentialEffects;
            
        private int RawResult { get; set; }
        public int Result => Mathf.Clamp(GetResultAfterMods(RawResult), 1, 6);
        
        protected abstract int GetResultAfterMods(int roll);

        protected DieRoll(Player roller, IStackEffect[] potentialEffects)
        {
            Roller = roller;
            _potentialEffects = potentialEffects;
            if (_potentialEffects.Length != 6)
            {
                Debug.LogError($"{this} Must have 6 potential effects!");
                throw new Exception();
            }  
        }

        public void ReRoll()
        {
            RawResult = Random.Range(1, 7);
            _stackMember.UpdateSprite(RawResult); // TODO: maybe use the final result instead? what is less confusing?
        }

        public void OnStackAdd()
        {
            ReRoll();
        }

        public void Resolve()
        {
            var effect = _potentialEffects[Result - 1];
            if (effect.MayResolve()) effect.Resolve();
            else effect.Fizzle();
            RollResolved?.Invoke(Result);
        }

        public string GetEffectText() // TODO: just show all the options? 
        {
            var text = $"Rolled a {RawResult}";
            if (RawResult != Result) text += $", effectively a {Result}";
            text += "\n" + _potentialEffects[Result - 1].GetEffectText();
            return text;
        }

        public GameObject GetStackVisual() 
        {
            var stackMember = Object.Instantiate(StackMemberClone.gameObject);
            _stackMember = stackMember.GetComponent<DieRollUI>();
            
            return stackMember;
        }

        public void Fizzle()
        {
            Debug.LogError($"{this} fizzled. That is not supposed to happen.");
            throw new Exception();
        }
    }
}