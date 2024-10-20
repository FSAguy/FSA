using System.Collections;
using foursoulsauto.core.player;

namespace foursoulsauto.core.effectlib
{
    // TODO: choosing the target of the attack is part of resolving the effect, NOT creating it
    // this requires players to be able to provide input *while the effect is resolving*
    // requires the GameStack to halt resolution while player is choosing
    // remember the card "Don't Starve" 
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

        public IEnumerator Resolve()
        {
            _attacker.AttacksRemaining--;
            var state = new AttackGamePhase(_target, _attacker);
            Board.Instance.Phase = state;

            yield return null;
        }

        public string GetEffectText()
        {
            return $"{_attacker.CharName} will attack {_target.CardName}";
        }
    }
}