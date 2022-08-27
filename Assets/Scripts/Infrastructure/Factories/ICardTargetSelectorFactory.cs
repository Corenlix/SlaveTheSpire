using Card.TargetSelectors;

namespace Infrastructure
{
    public interface ICardTargetSelectorFactory
    {
        CardTargetSelector ForType(CardTargetSelectorType type);
    }
}