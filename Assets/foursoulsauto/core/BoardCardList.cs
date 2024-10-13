using System.Collections.Generic;
using UnityEngine;

namespace foursoulsauto.core
{
    // Serializes a list of cards to be loaded on game start
    [CreateAssetMenu(fileName = "CardDatabase", menuName = "ScriptObj/CardDatabase", order = 1)]
    public class BoardCardList : ScriptableObject
    {
        [SerializeField] private List<Card> cards;

        public List<Card> Cards => cards;
    }
}
