using System.Collections.Generic;
using UnityEngine;
using static Core.Deck;

namespace Core
{
    public class DeckArrangement : MonoBehaviour
    {
        //TODO: add Deck -> DeckBehaviour dictionary
        [SerializeField] private DeckBehaviour loot;

        public void Setup(CardDatabase database)
        {
            // TODO: make this work for all decks
            var lootCards = database.Cards.FindAll(card => card.StartDeck == Loot).ConvertAll(Instantiate);
            loot.Setup(lootCards);
        }

        public void Draw(Deck deck, CardContainer container, int amount = 1)
        {
            if (deck == Loot)//TODO: see above TODO
            {
                loot.DrawInto(container, amount);
            }
        }

        public void Discard(Card card)
        {
            if (card.StartDeck == Loot) // TODO: you know the drill
            {
                loot.DiscardInto(card);
            }
        }
    }
}