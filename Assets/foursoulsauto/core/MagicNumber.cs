using UnityEngine;

namespace foursoulsauto.core
{
    // the main way to add numbers to card effects
    // this is the object to be manipulated by effects such as "magic marker"
    public class MagicNumber : MonoBehaviour
    {
        [SerializeField] private int initialValue;
        private int _value;

        private void Awake()
        {
            Value = initialValue;
        }

        // TODO: add animations when manipulated and graphics to showcase the new number
        // needs way to be selected (since there could be multiple numbers on a card)
        // this will include a text element on the card UI so be sure to place it correctly on the canvas
        public int Value
        {
            get => _value;
            set => _value = value;
        }
    }
}