using UnityEngine;

namespace foursoulsauto.core
{
    public interface IVisualStackEffect : IStackEffect
    {
        GameObject GetStackVisual();
    }
}