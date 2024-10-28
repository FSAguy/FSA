using System.Collections.Generic;

namespace foursoulsauto.core.board
{
    // for when a card seems to sort of not exist, a container between containers
    // very spooky
    // mainly just exists for loot cards being played, should probably change later
    public class VoidContainer : CardContainer
    {
        protected override void AfterCardsAdded(List<Card> addedCards)
        {
            addedCards.ForEach(card => card.HideCard());
        }

        protected override void AfterCardsRemoved(List<Card> removedCards) { }
    }
}