using System;
using UnityEngine;

namespace core
{
    public abstract class Card : MonoBehaviour
    {
        public event Action<bool> ChangedFace;
        private CardUI _ui;
        private bool _faceUp;

        [SerializeField] private Sprite topSprite;
        [SerializeField] private Sprite bottomSprite;
        [SerializeField] private Deck deck;

        [SerializeField] private GameObject cardPrefab;

        public Sprite TopSprite => topSprite;
        public Sprite BottomSprite => bottomSprite;
        public virtual CardAction PlayAction => null;
        public virtual CardAction TapAction => null;
        public Deck StartDeck => deck;
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

        public bool IsShown => gameObject.activeSelf; // TODO

        public bool FaceUp
        {
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
            FaceUp = false;
        }

    }
}
