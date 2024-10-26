using System.Collections;
using foursoulsauto.core.player;
using UnityEngine;

namespace foursoulsauto.core.effectlib
{
    // TODO: choosing the target of the attack is part of resolving the effect, NOT creating it
    // this requires players to be able to provide input *while the effect is resolving*
    // requires the GameStack to halt resolution while player is choosing
    // remember the card "Don't Starve" 
    public class AttackDeclarationEffect : IVisualStackEffect
    {
        private static readonly GameObject StackMemberClone = 
            Resources.Load<GameObject>("Prefabs/UI/AttackStackMember");
        
        private readonly Player _attacker;
        
        public AttackDeclarationEffect(Player attacker)
        {
            _attacker = attacker;
        }

        public bool MayResolve() =>
            _attacker.Character.IsAlive && _attacker.HasAttacksLeft;

        public IEnumerator Resolve()
        {
            // TODO: check that the player doesnt attack himself (dont think that is legal)
            // TODO: sometimes player is forced to attack something else ("Mom's Eyeshadow", "Krampus")
            var request = new EffectInput(card => card is LivingCard { IsAttackable: true });
            _attacker.RequestInput(request);
            yield return new WaitUntil(() => request.Filled);
            _attacker.AttacksRemaining--;
            var state = new AttackGamePhase(request.CardInput as LivingCard, _attacker);
            Board.Instance.Phase = state;

            yield return null;
        }

        public string GetEffectText()
        {
            return $"{_attacker.CharName} will attack";
        }

        public GameObject CreateStackVisual()
        {
            return Object.Instantiate(StackMemberClone);
        }
    }
}