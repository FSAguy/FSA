using System.Collections.Generic;
using foursoulsauto.core;
using UnityEngine;

namespace foursoulsauto.ui.player
{
    //TODO: Animate
    public class StackUI : PlayerUIModule
    {
        [SerializeField] private Transform stackPanel;
        
        private readonly Dictionary<IVisualStackEffect, GameObject> _effectToGameObject = new();

        protected override void Start()
        {
            base.Start();
            var stack = Board.Instance.Stack;
            stack.ItemPushed += OnItemPushed;
            stack.ItemFizzled += OnItemFizzled;
            stack.ItemResolved += OnItemFizzled; //TODO: make fizzled and resolved different (cool disintegration effect)
        }
                    
        private void OnItemFizzled(IVisualStackEffect obj)
        {
            var stackMember = _effectToGameObject[obj];
            _effectToGameObject.Remove(obj);
            Destroy(stackMember);
        }
        
        private void OnItemPushed(IVisualStackEffect obj)
        {
            var stackMember = obj.CreateStackVisual();
            stackMember.transform.SetParent(stackPanel);
            _effectToGameObject.Add(obj, stackMember);
        }
    }
}