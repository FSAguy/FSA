using System;

namespace foursoulsauto.core
{
    public class CardAction
    {
        public readonly Card Origin;
        private readonly Func<bool> _mayUseDelegate;
        public virtual string Text { get; private set; }
        public Func<EffectInput, IStackEffect> _generatorFunc;
        public EffectInput Input { get; }

        public CardAction(Card origin, Func<EffectInput, IStackEffect> generatorFunc, string text, 
            Func<bool> mayUseDelegate = null, EffectInput input = null)
        {
            Origin = origin;
            Text = text;
            _generatorFunc = generatorFunc;
            _mayUseDelegate = mayUseDelegate ?? (() => true);
            Input = input ?? new EffectInput();
        }

        public virtual bool MayUse => _mayUseDelegate() && Input.SomeInputExists;

        public virtual CardEffect GenerateEffect() => new(Origin, _generatorFunc(Input));
    }
}