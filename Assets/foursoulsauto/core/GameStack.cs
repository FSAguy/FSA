using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace foursoulsauto.core
{
    public class GameStack : MonoBehaviour
    {
        public event Action<IVisualStackEffect> ItemPushed;
        public event Action<IVisualStackEffect> ItemResolved;
        public event Action<IVisualStackEffect> ItemFizzled;

        private List<IVisualStackEffect> Stack { get; }

        public bool IsEmpty => Stack.Count == 0;

        public GameStack()
        {
            Stack = new List<IVisualStackEffect>();
        }

        public void Push(IVisualStackEffect effect)
        {
            if (effect == null)
            {
                Debug.LogError("TRIED TO ADD NULL EFFECT TO STACK");
                return;
            }
            Stack.Insert(0, effect);
            effect.OnStackAdd();// TODO: should be before ItemPushed.invoke, but then need to fix UI
            ItemPushed?.Invoke(effect); 
            Debug.Log($"Added effect: {effect.GetEffectText()}");
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

        private IEnumerator ResolveItem(IVisualStackEffect effect)
        {
            yield return effect.Resolve();
            ItemResolved?.Invoke(effect);
        }
    }
}