using foursoulsauto.core.player;

namespace foursoulsauto.core
{
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
            Board.Instance.Stack.Push(_currentAttack);
        }

        private void OnFightOver()
        {
            Board.Instance.Stack.CancelEffect(_currentAttack);
            Board.Instance.Phase = new NormalGamePhase(); // TODO: change once state is managed like a stack
        }
        
        public override void Enter()
        {
            AddAttackToStack();
        }

        public override void Leave()
        {
            // TODO (maybe Leave is unnecessary in general)
        }

        public override void EmptyStackPass()
        {
            AddAttackToStack();
        }
    }
}