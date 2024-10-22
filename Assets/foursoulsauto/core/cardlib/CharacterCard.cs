using UnityEngine;

namespace foursoulsauto.core.cardlib
{
    public abstract class CharacterCard : LivingCard
    {
        [SerializeField] protected Card StartingItem;
        
        // TODO: method to grab the starting item? maybe should be implemented at Player instead?
    }
}