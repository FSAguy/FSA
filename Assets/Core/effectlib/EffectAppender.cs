using System.Collections.Generic;

namespace core.effectlib
{
    public class EffectAppender : IEffect
    {
        private IEffect[] _effects;

        public EffectAppender(params IEffect[] effects)
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
            while (_effects[i].GetEffectText() == IEffect.NO_TEXT) 
                i++;
            
            var text = _effects[i++].GetEffectText();
            
            for (; i < _effects.Length; i++)
            {
                if (_effects[i].GetEffectText() != IEffect.NO_TEXT)
                {
                    text += ", then " + _effects[i].GetEffectText();
                }
            }

            return text;
        }
    }
}