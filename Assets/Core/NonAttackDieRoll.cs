using core.effectlib;

namespace core
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