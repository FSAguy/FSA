namespace foursoulsauto.core
{
    // the current state of the game which defines what the player can or cant do, and what happens when they pass
    // potential examples: the "dont starve" effect, effect involving voting, death? start/end turn? 
    // could be that all of that can be solved by manipulating card effects to include other players instead
    public abstract class GameState
    {
        // TODO: maybe include what players can or cant do?
        public abstract void Enter();
        public abstract void Leave();
        public abstract void EmptyStackPass();
    }
}