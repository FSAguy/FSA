using UnityEngine;

namespace foursoulsauto.core.cardlib
{
    public abstract class CharacterCard : LivingCard
    {
        [SerializeField] protected string startingItem;

        public string StartingItem => startingItem;

        // TODO: method to grab the starting item? maybe should be implemented at Player instead?
    }
}