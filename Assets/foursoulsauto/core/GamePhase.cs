namespace foursoulsauto.core
{
    // currently planned: start phase, action phase, end phase, attack phase? (probably should just be an effect that adds itself back to the stack)
    public abstract class GamePhase
    {
        // TODO: maybe include what players can or cant do? maybe unnecessary 
        public abstract void Enter();
        public abstract void Leave();

        public abstract void EmptyStackPass();

        public abstract string EmptyStackPassText { get; } // for stuff
        
    }
}