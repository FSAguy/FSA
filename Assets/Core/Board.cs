using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Board : MonoBehaviour
    {
        private static Board _instance = null;
        public static Board Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = FindObjectOfType<Board>();
                if (_instance != null) return _instance;
                Debug.LogError("Board object missing!");
                throw new Exception("Place the Board dingus!");
            }
        }
        
        [SerializeField] private List<Player> players;
        [SerializeField] private CardDatabase cardDatabase;
        [SerializeField] private DeckArrangement deckArrangement;
        
        private int _turnIdx;
        private void Start()
        {
            foreach (var player in players)
            {
               player.PlayerPassed += OnPlayerPassed;
            }
        
            deckArrangement.Setup(cardDatabase);
        }

        private void OnPlayerPassed()
        {
            throw new NotImplementedException();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
