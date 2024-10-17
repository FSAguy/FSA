using foursoulsauto.core.player;

namespace foursoulsauto.core.effectlib
{
    public class NonAttackDieRoll : DieRoll
    {

        protected override int GetResultAfterMods(int roll)
        {
            return roll; // TODO
        }

        public NonAttackDieRoll(Player roller, IStackEffect[] potentialEffects) : base(roller, potentialEffects)
        {
        }
    }
}