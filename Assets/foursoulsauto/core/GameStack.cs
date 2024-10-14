using System;
using System.Collections.Generic;
using UnityEngine;

namespace foursoulsauto.core
{
    public class GameStack
    {
        public event Action<IVisualStackEffect> ItemPushed;
        public event Action<IVisualStackEffect> ItemResolved;
        public event Action<IVisualStackEffect> ItemFizzled;

        public List<IVisualStackEffect> Stack { get; }


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
            ItemPushed?.Invoke(effect); 
            effect.OnStackAdd();// TODO: should be before ItemPushed.invoke, but then need to fix UI
            Debug.Log($"Added effect:loot {effect.GetEffectText()}");
        }

        public void Pop()
        {
            var topItem = Stack[0];
            Stack.Remove(topItem);
            
            if (topItem.MayResolve())
            {
                Debug.Log($"Activating Effect: {topItem.GetEffectText()}");
                topItem.Resolve();
                ItemResolved?.Invoke(topItem);
            }
            else
            {
                Debug.Log($"Effect fizzled: {topItem.GetEffectText()}");
                ItemFizzled?.Invoke(topItem);
            }
        }
    }
}