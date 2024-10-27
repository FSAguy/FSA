namespace foursoulsauto.core
{
    public class StartPhase : GamePhase
    {
        /*
         * 1. The Recharge Step. The active player recharges (turns upright) objects they control. Any objects controlled by the game (with the exception of shop items) also recharge.
         * 2. Abilities that trigger at the start of the turn trigger, then priority passes between all players.
         * 3. The Loot Step. The active player loots 1. At the end of this step, priority passes between all players, then the action phase begins.
         */
        
        public override void Enter()
        {
            // recharge
            // TODO: add "dont recharge" quality for some items (like car battery)
            Board.Instance.AllCards.ForEach(card => card.IsCharged = true);
            // TODO: start of turn abilities
        }

        public override void Leave()
        {
        }

        public override void EmptyStackPass()
        {
            throw new System.NotImplementedException();
        }

        public override string EmptyStackPassText { get; }
    }
}