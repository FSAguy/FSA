using foursoulsauto.core.effectlib;
using UnityEngine;

namespace foursoulsauto.core
{
    public class EndPhase : GamePhase
    {
        /*
        1.Abilities that trigger at the end of the turn trigger, then priority passes between all players.
        2.If the active player has more loot cards in their hand than their max hand size (10 by default), they discard loot cards until they have as many cards left in their hand as their max hand size.
        3.If playing with the room deck, and if a monster died during the turn, the active player may put a room into discard.
        4.The turn ends and is passed to the next player. All objects with an HP stat heal to full HP, including any dead players, and abilities and effects that last till end of turn end.
        */
        
        // TODO: max hand size? are there any cards that change it from 10?
        
        public override void Enter()
        {
            //discard down to 10 loot
            var target = Board.Instance.ActivePlayer;
            var discardEffect = new DiscardLootEffect(
                target, 
                () => Mathf.Max(0, target.Hand.Cards.Count - 10));
            // TODO: shouldn't be a card effect, should be its own thing
            Board.Instance.AddEffect(new CardEffect(Board.Instance.ActivePlayer.Character, discardEffect));
        }

        public override void Leave()
        {
            // brorg
        }

        public override void EmptyStackPass()
        {
            Board.Instance.BeginNextPlayerTurn();
        }

        public override string EmptyStackPassText => "End Turn";
    }
}