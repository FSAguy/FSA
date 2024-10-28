using System.Collections.Generic;
using UnityEngine;

namespace foursoulsauto.core.board
{
    // Serializes a list of cards to be loaded on game start
    [CreateAssetMenu(fileName = "CardDatabase", menuName = "FourSoulsAuto/CardDatabase")]
    public class BoardCardList : ScriptableObject
    {
        [SerializeField] private List<Card> cards;

        public List<Card> Cards => cards;
    }
}
