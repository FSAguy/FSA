namespace core
{
    public interface IEffect
    {
        const string NO_TEXT = "///";
        void Resolve();
        bool MayResolve() => true;
        void Fizzle()
        {
        }
        string GetEffectText();
    }
}