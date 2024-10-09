using UnityEngine;

namespace core
{
    public interface IStackEffect : IEffect
    {
        GameObject GetStackVisual();
    }
}