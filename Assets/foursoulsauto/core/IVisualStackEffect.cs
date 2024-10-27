using foursoulsauto.ui;
using UnityEngine;

namespace foursoulsauto.core
{
    public interface IVisualStackEffect : IStackEffect
    {
        StackMemberUI CreateStackVisual();
    }
}