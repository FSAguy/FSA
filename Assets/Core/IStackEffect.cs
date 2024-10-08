using UnityEngine;

namespace Core
{
    public interface IStackEffect
    {
        void Resolve();
        bool MayResolve() => true;

        void Fizzle()
        {
        }

        string GetEffectText();
        GameObject GetStackVisual();
    }
}