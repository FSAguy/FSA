using foursoulsauto.core.player;

namespace foursoulsauto.core.effectlib
{
    // TODO: make attackroll be created not when passing on an empty stack, but when there the stack BECOMES empty
    // (on stack emptied)
    public class AttackGamePhase : GamePhase
    {
        public LivingCard Defender { get; }
        public Player Attacker { get; }
        private IVisualStackEffect _currentAttack;

        public AttackGamePhase(LivingCard defender, Player attacker)
        {
            Defender = defender;
            Attacker = attacker;
            defender.Died += OnFightOver;
            attacker.Character.Died += OnFightOver;
        }

        public void Cancel()
        {
            OnFightOver();
        }

        private void AddAttackToStack()
        {
            _currentAttack = new AttackRoll(Attacker, Defender);
            Board.Instance.AddEffect(_currentAttack);
        }

        private void OnFightOver()
        {
            Board.Instance.Stack.CancelEffect(_currentAttack);
            Board.Instance.Phase = new ActionGamePhase(); // TODO: change once state is managed like a stack
        }
        
        public override void Enter()
        {
            AddAttackToStack(); //TODO: maybe remove the comment? idk
        }

        public override void Leave()
        {
            // TODO (maybe Leave is unnecessary in general)
        }

        public override void EmptyStackPass()
        {
            AddAttackToStack();
        }

        public override string EmptyStackPassText => "Roll Attack";
    }
}