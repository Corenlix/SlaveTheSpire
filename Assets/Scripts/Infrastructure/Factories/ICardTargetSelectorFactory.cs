using Cards.TargetSelectors;

namespace Infrastructure.Factories
{
    public interface ICardTargetSelectorFactory
    {
        CardTargetSelector ForType(CardTargetSelectorType type);
    }
}