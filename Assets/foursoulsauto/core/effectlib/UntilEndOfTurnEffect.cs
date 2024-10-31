using System.Collections;

namespace foursoulsauto.core.effectlib
{
    public abstract class UntilEndOfTurnEffect : IStackEffect
    {
        protected abstract void EndOfTurnCleanup();
        
        public IEnumerator Resolve()
        {
            Board.Instance.TurnEndCleanup += EndOfTurnCleanup;
            yield return ApplyEndOfTurnEffect();
        }

        protected abstract IEnumerator ApplyEndOfTurnEffect();

        public abstract string GetEffectText();
    }
}