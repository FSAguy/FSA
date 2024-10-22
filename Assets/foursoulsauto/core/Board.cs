using System;
using System.Collections.Generic;
using foursoulsauto.core.deck;
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
        [SerializeField] private BoardCardList boardCardList;
        [SerializeField] private DeckArrangement deckArrangement;
        
        public GameStack Stack { get; private set; }
        public Player ActivePlayer => players[_activeIdx];
        public Player PriorityPlayer => players[_priorityIdx];
        public List<Card> AllCards => deckArrangement.AllCards;

        public int PlayerCount => players.Count;

        // TODO: game phase should probably have a stack of its own
        // for example, you always have the "normal" phase at the bottom
        // when you pop the normal phase, you add the "end turn" phase, which when popped adds the "start turn" phase
        // when attacking, we push the "attack" phase, etc
        public GamePhase Phase
        {
            get => _phase;
            set
            {
                _phase.Leave();
                _phase = value; 
                _phase.Enter();
            }
        }

        //TODO: properly change theses on turn switch
        private int _activeIdx; // who's turn is it
        private int _priorityIdx; // who has priority
        private int _popIdx; // when will the stack next pop
        private VoidContainer _voidContainer;
        private GamePhase _phase;

        private void Awake()
        {
            Stack = gameObject.AddComponent<GameStack>();
            _voidContainer = gameObject.AddComponent<VoidContainer>();
            foreach (var player in players)
            {
               player.PlayerPassed += OnPlayerPassed;
            }
            deckArrangement.Setup(boardCardList);
        }

        private void Start() // TODO: move this into a separate "begin game" method, recheck values
        {
            _phase = new NormalGamePhase();
            foreach (var player in players)
            {
               player.Cents = 0;
            }
            PlayerLoot(players[0], 3);
            _activeIdx = -1; // because new turn increments it
            NewTurn();
        }

        private void NewTurn()
        {
            _activeIdx = (_activeIdx + 1) % PlayerCount;
            _popIdx = _priorityIdx = _activeIdx;
            PriorityPlayer.HasPriority = true;
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
            // todo: maybe prevent passing by incorrect player?
            Debug.Log($"{PriorityPlayer.CharName} index {_priorityIdx} passed");
            if (Stack.IsEmpty) Phase.EmptyStackPass();
            else
            {
                PriorityPlayer.HasPriority = false;
                _priorityIdx = (_priorityIdx + 1) % PlayerCount;
                if (_priorityIdx == _popIdx) Stack.Pop(); 
            }

            Debug.Log($"{_priorityIdx} turn");
            PriorityPlayer.HasPriority = true;
        }

        public void AddEffect(IVisualStackEffect effect)
        {
            Stack.Push(effect);
            _popIdx = _priorityIdx;
        }
    }
}
