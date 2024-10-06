using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class DeckArrangement : MonoBehaviour
    {
        [SerializeField] private DeckBehaviour loot;

        public void Setup(CardDatabase database)
        {
            // TODO: make this work for all decks
            var lootCards = database.Cards.FindAll(card => card.StartDeck == Deck.Loot).ConvertAll(Instantiate);
            loot.Setup(lootCards);
        }
    }
}