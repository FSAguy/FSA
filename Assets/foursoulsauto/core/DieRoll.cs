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
        
        protected Player Roller;
        
        private DieRollUI _stackMember;
            
        private int RawResult { get; set; }
        public int Result => Mathf.Clamp(GetResultAfterMods(RawResult), 1, 6);
        
        protected abstract int GetResultAfterMods(int roll);

        protected DieRoll(Player roller)
        {
            Roller = roller;
        }

        public void ReRoll()
        {
            RawResult = Random.Range(1, 7);
            _stackMember.UpdateSprite(RawResult);
        }

        public void OnStackAdd()
        {
            ReRoll();
        }

        public void Resolve()
        {
            RollResolved?.Invoke(Result);
        }

        public string GetEffectText()
        {
            var text = $"Rolled a {RawResult}";
            if (RawResult != Result) text += $", effectively a {Result}";
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