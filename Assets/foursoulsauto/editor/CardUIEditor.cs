using foursoulsauto.core;
using foursoulsauto.ui;
using UnityEditor;
using UnityEngine;

namespace foursoulsauto.editor
{
    // TODO: manipulate stats after changing them
    [CustomEditor(typeof(CardUI), true)]
    public class CardUIEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
        
            var ui = (CardUI)target;
        
            var card = ui.gameObject.GetComponent<Card>();
            if (card == null) return;
        
            var renderer = ui.GetComponentInChildren<SpriteRenderer>();
            if (renderer == null) return;
        
            renderer.sprite = card.TopSprite;
            EditorUtility.SetDirty(renderer);
        }
    }
}