namespace foursoulsauto.core
{
    // the usual game state where you can play cards, start fights, buy from shops, etc
    public class ActionPhase : GamePhase
    {
        public override void Enter()
        {
            // TODO: loot 1, gain 1 loot play
        }

        public override void Leave()
        {
            // bruh...
        }

        public override void EmptyStackPass()
        {
            Board.Instance.Phase = new EndPhase();
        }

        public override string EmptyStackPassText => "Enter End Phase";
    }
}