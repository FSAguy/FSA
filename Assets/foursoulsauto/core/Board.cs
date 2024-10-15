using System;
using System.Collections.Generic;
using foursoulsauto.core.player;
using UnityEngine;
using UnityEngine.Serialization;

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
        [SerializeField] private BoardCardList boardCardList;
        [SerializeField] private DeckArrangement deckArrangement;
        
        public GameStack Stack { get; private set; }
        public Player ActivePlayer => players[_turnIdx];
        public Player PriorityPlayer => players[_priorityIdx];
        public List<Card> AllCards => deckArrangement.AllCards;

        // TODO: game state should probably have a stack of its own
        // for example, you always have the "normal" state at the bottom
        // when you pop the normal state, you add the "end turn" state, which when popped adds the "start turn" state
        // when attacking, we push the "attack" state, etc
        public GameState State
        {
            get => _state;
            set
            {
                _state.Leave();
                _state = value; 
                _state.Enter();
            }
        }

        //TODO: properly change theses on turn switch
        private int _turnIdx; 
        private int _priorityIdx;
        private VoidContainer _voidContainer;
        [SerializeField] private GameState _state;

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
            _state = new NormalGameState();
            foreach (var player in players)
            {
               player.Cents = 0;
               
            }
            deckArrangement.Setup(boardCardList);
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
            if (Stack.IsEmpty) State.EmptyStackPass();
            else Stack.Pop(); 
        }

        public void PlayEffect(IVisualStackEffect effect)
        {
            Stack.Push(effect);
        }

    }
}
