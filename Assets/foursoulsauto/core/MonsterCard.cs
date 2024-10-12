namespace foursoulsauto.core
{
    public abstract class MonsterCard : LivingCard
    {
        protected abstract CardEffect GenerateRewards();

        protected override void OnDeath()
        {
            base.OnDeath();
            Board.Instance.Stack.Push(GenerateRewards());
        }
    }
}