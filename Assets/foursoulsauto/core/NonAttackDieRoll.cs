using foursoulsauto.core.player;

namespace foursoulsauto.core
{
    public class NonAttackDieRoll : DieRoll
    {
        public NonAttackDieRoll(Player roller) : base(roller)
        {
        }

        protected override int GetResultAfterMods(int roll)
        {
            return roll; // TODO
        }
    }
}