using foursoulsauto.core;
using foursoulsauto.ui;
using UnityEditor;
using UnityEngine;

namespace foursoulsauto.editor
{
    [CustomEditor(typeof(CardUI))]
    public class CardUIEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        
            var ui = (CardUI)target;
        
            var card = ui.gameObject.GetComponentInParent<Card>();
            if (card == null) return;
        
            var renderer = card.GetComponentInChildren<SpriteRenderer>();
            if (renderer == null) return;
        
            renderer.sprite = card.TopSprite;
            EditorUtility.SetDirty(renderer);
        }
    }
}