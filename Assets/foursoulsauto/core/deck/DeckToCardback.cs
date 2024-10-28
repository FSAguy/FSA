using System;
using System.Collections.Generic;
using UnityEngine;

namespace foursoulsauto.core.deck
{
    [CreateAssetMenu(fileName = "CardbackTable", menuName = "FourSoulsAuto/CardbackTable")]
    public class DeckToCardback : ScriptableObject
    {
        public Sprite Loot;
        public Sprite Monster;
        public Sprite Treasure;
        public Sprite Character;
        public Sprite StarterItem;
        public Sprite BonusSoul;
        public Sprite Room;

        private Dictionary<Deck, Sprite> _dict;

        private void OnEnable()
        {
            _dict = new Dictionary<Deck, Sprite>
            {
                { Deck.Loot, Loot },
                { Deck.Monster, Monster },
                { Deck.Treasure, Treasure },
                { Deck.Character, Character },
                { Deck.StartingItem, StarterItem },
                { Deck.BonusSouls, BonusSoul },
                { Deck.Room, Room }
            };
        }

        public Sprite Get(Deck deck)
        {
            return _dict[deck];
        }
    }
}