using System.Collections.Generic;
using UnityEngine;

namespace foursoulsauto.core
{
    [CreateAssetMenu(fileName = "CardDatabase", menuName = "ScriptObj/CardDatabase", order = 1)]
    public class CardDatabase : ScriptableObject
    {
        [SerializeField] private List<Card> cards;

        public List<Card> Cards => cards;
    }
}
