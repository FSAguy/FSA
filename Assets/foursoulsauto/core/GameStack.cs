using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using foursoulsauto.core.effectlib;
using UnityEngine;

namespace foursoulsauto.core
{
    public class GameStack : MonoBehaviour
    {
        public event Action<IVisualStackEffect> ItemPushed;
        public event Action<IVisualStackEffect> ItemResolved;
        public event Action<IVisualStackEffect> ItemFizzled;
        public event Action<IVisualStackEffect> ItemAboutToPop;

        private List<IVisualStackEffect> Stack { get; } = new();
        public List<IVisualStackEffect> NewlyAdded { get; private set; } = new();

        // TODO: implement "may reorder" according to four souls rules - requires effect tags
        public bool MayReorder => NewlyAdded.Count > 0; 

        public bool IsEmpty => Stack.Count == 0;

        public void FlushNewlyAdded()
        {
            foreach (var effect in NewlyAdded)
            {
                if (effect == null)
                {
                    Debug.LogError("TRIED TO ADD NULL EFFECT TO STACK");
                    return;
                }
                Stack.Insert(0, effect);
                effect.OnStackAdd();
                Debug.Log($"Added Effect: {effect.GetEffectText()}");
                ItemPushed?.Invoke(effect); 
            }

            NewlyAdded = new List<IVisualStackEffect>();
        }

        public void Push(IVisualStackEffect effect)
        {
            NewlyAdded.Add(effect);
        }

        public void CancelEffect(IVisualStackEffect effect)
        {
            if (Stack.Remove(effect)) ItemFizzled?.Invoke(effect); // no guarantee that the effect is still in stack
        }

        public IVisualStackEffect GetFirstWhere(Func<IVisualStackEffect, bool> predicate)
        {
            return Stack.FirstOrDefault(predicate);
        }

        public void Pop()
        {
            var topItem = Stack[0];
            Stack.Remove(topItem);
            
            topItem.OnLeaveStack();
            
            if (topItem.MayResolve())
            {
                Debug.Log($"Activating Effect: {topItem.GetEffectText()}");
                StartCoroutine(ResolveItem(topItem));
            }
            else
            {
                Debug.Log($"Effect fizzled: {topItem.GetEffectText()}");
                ItemFizzled?.Invoke(topItem);
            }
        }

        public DieRoll TopRollEffect => GetFirstWhere(effect => effect is DieRoll) as DieRoll;
        
        public bool HasRoll => TopRollEffect is not null;

        public void WarnPop()
        {
            ItemAboutToPop?.Invoke(Stack[0]);
        }

        private IEnumerator ResolveItem(IVisualStackEffect effect)
        {
            yield return effect.Resolve();
            ItemResolved?.Invoke(effect);
        }
    }
}