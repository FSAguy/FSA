using UnityEngine;

namespace foursoulsauto.core
{
    // the usual game state where you can play cards, start fights, buy from shops, etc
    public class ActionGamePhase : GamePhase
    {
        public override void Enter()
        {
           // uhhh
        }

        public override void Leave()
        {
            // bruh...
        }

        public override void EmptyStackPass()
        {
            // TODO: enter end turn state
            Debug.Log("le turn end");
        }

        public override string EmptyStackPassText => "End Turn";
    }
}