using UnityEngine;

namespace foursoulsauto.core.player
{
    public class CardGridContainer : CardContainer
    {
        [SerializeField] private BoxCollider zone;
        [SerializeField] private int rows;
        [SerializeField] private int cols;

        private float RowLength => zone.size.y / rows;
        private float ColLength => zone.size.x / cols;

        
        protected override void Add(Card card)
        {
            base.Add(card);
            
        }
    }
}