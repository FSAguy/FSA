namespace foursoulsauto.core.player
{
    public abstract class PlayerCardContainer : CardContainer
    {
        private Player _player;
        public sealed override Player Owner => _player;

        protected virtual void Awake()
        {
            _player = GetComponentInParent<Player>();
        }
    }
}