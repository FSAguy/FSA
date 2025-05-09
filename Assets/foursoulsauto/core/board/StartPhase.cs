using foursoulsauto.core.effectlib;

namespace foursoulsauto.core.board
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
            Board.Instance.ActivePlayer.ActiveCards.ForEach(card => card.IsCharged = true);
            Board.Instance.ActivePlayer.LootPlaysRemaining++;
            Board.Instance.ActivePlayer.AttacksRemaining++;
            // TODO: start of turn abilities
            // TODO: not a card effect
            // TODO: should not just equal 1, may include other effects (
            Board.Instance.AddEffectAndFlush(new CardEffect(Board.Instance.ActivePlayer.Character,
                new PlayersGainLootEffect(Board.Instance.ActivePlayer, ()=>1)));
            
        }

        public override void Leave()
        {
            // :)
        }

        public override void EmptyStackPass()
        {
            Board.Instance.Phase = new ActionPhase();
        }

        public override string EmptyStackPassText => "Begin Active Phase";
    }
}