using UnityEngine;

namespace core
{
    public abstract class Card : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private bool _faceUp;

        [SerializeField] private Sprite topSprite;
        [SerializeField] private Sprite bottomSprite;
        [SerializeField] private Deck deck;
        
        public Sprite CurrentSprite => _renderer.sprite;
        
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
                _renderer.sprite = value ? topSprite : bottomSprite;
                _faceUp = value;
            }
        }

        public void Discard()
        {
            Board.Instance.Discard(this);
        }

        protected virtual void Awake()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
            FaceUp = false;
        }

    }
}
