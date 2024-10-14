using System.Collections.Generic;
using UnityEngine;
using static foursoulsauto.core.Deck;

namespace foursoulsauto.core
{
    public class DeckArrangement : MonoBehaviour
    {
        //TODO: add Deck -> DeckBehaviour dictionary
        [SerializeField] private DeckBehaviour loot;
        [SerializeField] private DeckBehaviour monster;

        public List<Card> AllCards { get; private set; } 

        public void Setup(BoardCardList list)
        {
            // TODO: use dictionary to loop over starter decks
            AllCards = list.Cards.ConvertAll(Instantiate);
            var lootCards = AllCards.FindAll(card => card.StartDeck == Loot);
            loot.Setup(lootCards);
            var monsterCards= AllCards.FindAll(card => card.StartDeck == Monster);
            monster.Setup(monsterCards);
        }

        public void Draw(Deck deck, CardContainer container, int amount = 1)
        {
            if (deck == Loot)//TODO: see above TODO
            {
                loot.DrawInto(container, amount);
            } else if (deck == Monster)
            {
                monster.DrawInto(container, amount);
            }
        }

        public void Discard(Card card)
        {
            if (card.StartDeck == Loot) // TODO: you know the drill
            {
                loot.DiscardInto(card);
            } else if (card.StartDeck == Monster)
            {
                monster.DiscardInto(card);
            }
        }
    }
}