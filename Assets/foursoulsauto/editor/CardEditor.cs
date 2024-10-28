using System;
using foursoulsauto.core;
using foursoulsauto.core.deck;
using UnityEditor;
using UnityEngine;

namespace foursoulsauto.editor
{
    [CustomEditor(typeof(Card), true)]
    public class CardEditor : Editor
    {
        private SerializedProperty _bottomSprite;
        private SerializedProperty _deck;
        private bool _useDefaultBacksprite;
        
        private void OnEnable()
        {
            _bottomSprite = serializedObject.FindProperty("bottomSprite");
            _deck = serializedObject.FindProperty("deck");
            _useDefaultBacksprite = true;
        }

        public override void OnInspectorGUI()
        {
            // TODO: remove editing the bottom sprite if toggle is off?
            base.OnInspectorGUI();
            _useDefaultBacksprite = EditorGUILayout.Toggle("Use default bottom sprite", _useDefaultBacksprite);

            if (_useDefaultBacksprite)
            {
                _bottomSprite.objectReferenceValue = Board.Instance.GetCardback((Deck)_deck.intValue);
                Debug.Log($"byoying {Board.Instance.GetCardback((Deck)_deck.intValue)}");
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}