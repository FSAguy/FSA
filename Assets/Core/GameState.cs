namespace core
{
    public abstract class GameState
    {
        // TODO: maybe include what players can or cant do?
        public abstract void Enter();
        public abstract void Leave();
        public abstract void EmptyStackPass();
    }
}