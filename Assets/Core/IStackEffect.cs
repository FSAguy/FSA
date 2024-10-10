namespace core
{
    public interface IStackEffect
    {
        const string NO_TEXT = "///";
        void Resolve();
        bool MayResolve() => true;
        void Fizzle() { }
        void OnStackAdd() { }
        string GetEffectText();
    }
}