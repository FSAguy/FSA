namespace foursoulsauto.core
{
    // the current phase of the game which defines what the player can or cant do, and what happens when they pass
    // potential examples: the "dont starve" effect, effect involving voting, death? start/end turn? 
    public abstract class GamePhase
    {
        // TODO: maybe include what players can or cant do? maybe unnecessary 
        public abstract void Enter();
        public abstract void Leave();
        public abstract void EmptyStackPass();
    }
}