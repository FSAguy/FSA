using System.Collections.Generic;
using UnityEngine;
using static foursoulsauto.core.Deck;

namespace foursoulsauto.core
{
    public class DeckArrangement : MonoBehaviour
    {
        //TODO: add Deck -> DeckBehaviour dictionary
        [SerializeField] private DeckBehaviour loot;

        public List<Card> AllCards { get; private set; } 

        public void Setup(BoardCardList list)
        {
            // TODO: make this work for all decks
            AllCards = list.Cards.ConvertAll(Instantiate);
            var lootCards = AllCards.FindAll(card => card.StartDeck == Loot);
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