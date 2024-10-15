namespace foursoulsauto.core
{
    public interface IStackEffect
    {
        const string NO_TEXT = "///";
        void Resolve();
        bool MayResolve() => true;
        void OnLeaveStack() { }
        void OnStackAdd() { }
        string GetEffectText();
    }
}