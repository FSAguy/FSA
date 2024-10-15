using System;
using System.Collections.Generic;
using foursoulsauto.ui;
using UnityEngine;

namespace foursoulsauto.core
{
    public abstract class Card : MonoBehaviour
    {
        public event Action<bool> ChangedFace;
        private CardUI _ui;
        private bool _faceUp;

        [SerializeField] private Sprite topSprite;
        [SerializeField] private Sprite bottomSprite;
        [SerializeField] private Deck deck;
        [SerializeField] private string cardName;
        [SerializeField] private GameObject cardPrefab;

        public Sprite TopSprite => topSprite;
        public Sprite BottomSprite => bottomSprite;
        
        public virtual List<CardAction> Actions => new();
        public Deck StartDeck => deck;

        public string CardName => cardName;
        // NEVER MUTATE DIRECTLY - USE CardContainer.MoveInto
        // probably dumb programming
        public CardContainer Container { get; set; }

        public void HideCard()
        {
            gameObject.SetActive(false);//TODO
        }

        public void ShowCard()
        {
            gameObject.SetActive(true);//TODO
        }

        //TODO: add "in play" parameter, maybe as replacement
        public bool IsShown => gameObject.activeSelf; // TODO

        public bool FaceUp // TODO: should differ for clients in multiplayer
        { //gee, cant wait for writing multiplayer code wheehee
            get => _faceUp;
            set
            {
                _faceUp = value;
                ChangedFace?.Invoke(value);
            }
        }

        public void Discard()
        {
            Board.Instance.Discard(this);
        }

        protected virtual void Awake()
        {
            var body = Instantiate(cardPrefab, transform);
            body.transform.localPosition = Vector3.zero;
            _ui = GetComponentInChildren<CardUI>();
            _ui.Subscribe(this);
            FaceUp = true; // TODO: should be false, true for testing
        }

    }
}
