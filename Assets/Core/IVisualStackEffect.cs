using UnityEngine;

namespace core
{
    public interface IVisualStackEffect : IStackEffect
    {
        GameObject GetStackVisual();
    }
}