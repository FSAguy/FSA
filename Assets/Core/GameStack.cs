using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class GameStack
    {
        public event Action<IStackEffect> ItemPushed;
        public event Action<IStackEffect> ItemResolved;
        public event Action<IStackEffect> ItemFizzled;

        public List<IStackEffect> Stack { get; }

        public IStackEffect TopItem => Stack[0];

        public GameStack()
        {
            Stack = new List<IStackEffect>();
        }

        public void Push(IStackEffect effect)
        {
            if (effect == null)
            {
                Debug.LogError("TRIED TO ADD NULL EFFECT TO STACK");
                return;
            }
            Stack.Insert(0, effect);
            ItemPushed?.Invoke(effect);
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