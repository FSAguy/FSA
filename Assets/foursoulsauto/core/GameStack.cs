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

        public IVisualStackEffect TopItem => Stack[0];

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
            effect.OnStackAdd();
            Debug.Log($"Added effect: {effect.GetEffectText()}");
        }

        public void Pop()
        {
            if (Stack[0].MayResolve())
            {
                Debug.Log($"Activating Effect: {Stack[0].GetEffectText()}");
                Stack[0].Resolve();
                ItemResolved?.Invoke(Stack[0]);
            }
            else
                ItemFizzled?.Invoke(Stack[0]);

            Stack.Remove(TopItem);
        }
    }
}