using foursoulsauto.core.player;

namespace foursoulsauto.core.effectlib
{
    public class AttackDeclarationEffect : IStackEffect
    {
        private Player _attacker;
        private LivingCard _target;
        
        public AttackDeclarationEffect(Player attacker, LivingCard target)
        {
            _target = target;
            _attacker = attacker;
        }

        public bool MayResolve() =>
            _attacker.Character.IsAlive && _attacker.HasAttacksLeft && _target.IsAttackable;

        public void Resolve()
        {
            _attacker.AttacksRemaining--;
            var state = new AttackGamePhase(_target, _attacker);
            Board.Instance.Phase = state;
        }

        public string GetEffectText()
        {
            return $"{_attacker.CharName} will attack {_target.CardName}";
        }
    }
}