using System.Collections;

namespace foursoulsauto.core
{
    public interface IStackEffect
    {
        const string NO_TEXT = "///";
        IEnumerator Resolve();
        bool MayResolve() => true;
        void OnLeaveStack() { }
        void OnStackAdd() { }
        string GetEffectText();
    }
}