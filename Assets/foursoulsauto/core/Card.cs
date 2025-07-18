﻿using System;
using System.Collections.Generic;
using foursoulsauto.core.deck;
using foursoulsauto.core.player;
using foursoulsauto.ui;
using UnityEngine;

namespace foursoulsauto.core
{
    public abstract class Card : MonoBehaviour
    {
        public event Action<bool> ChangedFace;
        public event Action<bool> ChangedCharge;
        
        private CardUI _ui;
        private bool _faceUp;
        private bool _charged;

        [SerializeField] private Sprite topSprite;
        [SerializeField] private Sprite bottomSprite;
        [SerializeField] private Deck deck;
        [SerializeField] private string cardName;
        [SerializeField] private List<CardTag> tags;

        public List<CardTag> Tags => tags;

        public Sprite TopSprite => topSprite;
        public Sprite BottomSprite => bottomSprite;
        public Deck StartDeck => deck;
        public string CardName => cardName;
        public Player Owner => Container.Owner;

        public virtual List<CardAction> Actions => new();
        public virtual MagicNumber[] MagicNumbers => GetComponentsInChildren<MagicNumber>();
        
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

        public bool IsCharged
        {
            get => _charged;
            set
            {
                if (_charged == value) return;
                _charged = value;
                ChangedCharge?.Invoke(_charged);
            }
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
            FaceUp = true; // TODO: should be false, true for testing
            _charged = true; 
        }
    }
}
