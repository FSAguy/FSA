using System.Collections;
using foursoulsauto.core.deck;

namespace foursoulsauto.core.effectlib
{
    public class RefillSlotEffect : DeckEffect
    {
        private CardPile _slot;

        public RefillSlotEffect(DeckBehaviour deckBehaviour, CardPile slot) : base(deckBehaviour)
        {
            _slot = slot;
        }

        public override IEnumerator Resolve()
        {
            DeckBehaviour.DrawInto(_slot, 1);
            yield break;
        }

        public override string GetEffectText()
        {
            return $"the slot is refilled";
        }
    }
}