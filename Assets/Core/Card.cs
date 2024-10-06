using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Core
{
    public class Card : MonoBehaviour
    {
        public const float MOVETIME = 2f; // TODO: make this depend on game settings (game speed?)
        
        private SpriteRenderer _renderer;
        private bool _faceUp = false;
        
        [SerializeField] private Sprite topSprite;
        [SerializeField] private Sprite bottomSprite;
        [SerializeField] private Deck deck;

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

        public void MoveTo(Vector3 pos, Quaternion rot, float time)
        {
            LeanTween.cancel(gameObject);
            LeanTween.move(gameObject, pos, time).setEaseInOutExpo();
            LeanTween.rotate(gameObject, rot.eulerAngles, time).setEaseInExpo();
        }

        public void MoveTo(Transform t, float time = MOVETIME)
        {
            MoveTo(t.position, t.rotation, time);
        }

        private void Awake()
        {
            _renderer = GetComponentInChildren<SpriteRenderer>();
            FaceUp = false;
        }
    }
}
