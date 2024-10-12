using System;
using System.Collections.Generic;
using foursoulsauto.core.player;
using UnityEngine;

namespace foursoulsauto.core
{
    public class Board : MonoBehaviour
    {
        private static Board _instance;
        public static Board Instance
        {
            get
            {
                if (_instance is not null) return _instance;
                _instance = FindObjectOfType<Board>();
                if (_instance != null) return _instance;
                Debug.LogError("Board object missing!");
                throw new Exception("Place the Board dingus!");
            }
        }
        
        [SerializeField] private List<Player> players;
        [SerializeField] private CardDatabase cardDatabase;
        [SerializeField] private DeckArrangement deckArrangement;
        public GameStack Stack { get; private set; }
        public Player ActivePlayer => players[_turnIdx];
        public Player PriorityPlayer => players[_priorityIdx];
        
        private int _turnIdx; //TODO: properly use this
        private int _priorityIdx;
        private VoidContainer _voidContainer;

        private void Awake()
        {
            Stack = new GameStack();
            _voidContainer = gameObject.AddComponent<VoidContainer>();
            foreach (var player in players)
            {
               player.PlayerPassed += OnPlayerPassed;
            }
        }

        private void Start()
        {
            foreach (var player in players)
            {
               player.Cents = 0;
               
            }
            deckArrangement.Setup(cardDatabase);
            PlayerLoot(players[0], 3);
        }

        private void PlayerLoot(Player player, int amount)
        {
            deckArrangement.Draw(Deck.Loot, player.Hand, amount);
        }

        public void Discard(Card card)
        {
            deckArrangement.Discard(card);
        }

        // not sure if anything other than playing loot will use this
        public void VoidCard(Card card)
        {
            _voidContainer.MoveInto(card);
        }
        
        private void OnPlayerPassed()
        {
            Stack.Pop(); // TODO: change lol
        }

        public void PlayEffect(IVisualStackEffect effect)
        {
            Stack.Push(effect);
        }

    }
}
