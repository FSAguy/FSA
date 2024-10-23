using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static foursoulsauto.core.deck.Deck;

namespace foursoulsauto.core.deck
{
    public class DeckArrangement : MonoBehaviour
    {
        [SerializeField] private DeckBehaviour loot;
        [SerializeField] private DeckBehaviour monster;
        [SerializeField] private DeckBehaviour character;
        [SerializeField] private DeckBehaviour startingItem;

        private Dictionary<Deck, DeckBehaviour> _decktionairy; // absolutely hysterical

        public List<Card> AllCards { get; private set; }

        private void Awake()
        {
            _decktionairy = new Dictionary<Deck, DeckBehaviour>
            {
                { Loot, loot }, 
                { Monster, monster }, 
                { Character, character }, 
                { StartingItem, startingItem }
            };
        }

        public void Setup(BoardCardList list)
        {
            AllCards = list.Cards.ConvertAll(Instantiate);
            foreach (var (deck, behaviour) in _decktionairy)
            {
                var cards = AllCards.FindAll(card => card.StartDeck == deck);
                behaviour.Setup(cards);
            }
        }

        public Card GetTopOf(Deck deck)
        {
            return _decktionairy[deck].TopDraw;
        }

        public Card FindInDeck(Deck deck, Predicate<Card> predicate)
        {
            return _decktionairy[deck].DrawCards.Find(predicate);
        }

        public void Draw(Deck deck, CardContainer container, int amount = 1)
        {
            _decktionairy[deck].DrawInto(container, amount);
        }

        public void Discard(Card card)
        {
            _decktionairy[card.StartDeck].DiscardInto(card);
        }
    }
}