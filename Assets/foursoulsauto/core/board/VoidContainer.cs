using System.Collections.Generic;

namespace foursoulsauto.core.board
{
    // for when a card seems to sort of not exist, a container between containers
    // very spooky
    // TODO: magic marker should be able to target loot being played, therefore loot cards must be shown somewhere. Create proper holding place for loot being played.
    public class VoidContainer : CardContainer
    {
        protected override void AfterCardsAdded(List<Card> addedCards)
        {
            addedCards.ForEach(card => card.HideCard());
        }

        protected override void AfterCardsRemoved(List<Card> removedCards) { }
    }
}