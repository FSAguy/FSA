using UnityEngine;

namespace foursoulsauto.core.cardlib
{
    public abstract class CharacterCard : LivingCard
    {
        [SerializeField] protected string startingItem;

        public string StartingItem => startingItem;

        protected override void OnDeath()
        {
            // TODO: player death
        }
    }
}