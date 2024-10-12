namespace foursoulsauto.core.effectlib
{
    // wrapper effect for chaining multiple effects together
    public class EffectAppender : IStackEffect
    {
        private IStackEffect[] _effects;

        public EffectAppender(params IStackEffect[] effects)
        {
            _effects = effects;
        }
        public void Resolve()
        {
            foreach (var effect in _effects) effect.Resolve();
        }

        public string GetEffectText()
        {
            var i = 0;
            while (_effects[i].GetEffectText() == IStackEffect.NO_TEXT) 
                i++;
            
            var text = _effects[i++].GetEffectText();
            
            for (; i < _effects.Length; i++)
            {
                if (_effects[i].GetEffectText() != IStackEffect.NO_TEXT)
                    text += ", then " + _effects[i].GetEffectText();
            }

            return text;
        }

        public void OnStackAdd()
        {
            foreach (var effect in _effects) effect.OnStackAdd();
        }
    }
}