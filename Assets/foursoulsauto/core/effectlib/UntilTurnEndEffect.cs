using System.Collections;

namespace foursoulsauto.core.effectlib
{
    //TODO: this
    //requires some tashtit to remember the effect so it could cancel it once turn ends
    // maybe should be abstract and have a "on turn end" abstract func
    // maybe just a wrapper which accepts some "cleanup object" that sticks to the end of turn
    
    // wrapper (superclass?) effect for persistent effect disappearing at the end of the turn
    public class UntilTurnEndEffect : IStackEffect
    {
        public IEnumerator Resolve()
        {
            throw new System.NotImplementedException();
        }

        public string GetEffectText()
        {
            throw new System.NotImplementedException();
        }
    }
}