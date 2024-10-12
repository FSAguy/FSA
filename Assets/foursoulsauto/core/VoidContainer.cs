using System.Collections.Generic;

namespace foursoulsauto.core
{
    // for when a card seems to sort of not exist, a container between containers
    // very spooky
    // mainly just exists for loot cards being played, should probably change later
    public class VoidContainer : BoardCardContainer
    {
        public override ContainerType ConType => ContainerType.Nothing;
        protected override void Add(Card card)
        {
            base.Add(card);
            card.HideCard();
        }

        protected override void Add(List<Card> cards)
        {
            base.Add(cards);
            cards.ForEach(card => card.HideCard());
        }
    }
}