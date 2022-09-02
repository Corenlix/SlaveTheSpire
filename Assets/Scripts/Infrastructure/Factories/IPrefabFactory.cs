using Cards.TargetSelectors;

namespace Infrastructure.Factories
{
    public interface IPrefabFactory
    {
        CardTargetSelector ForType(CardTargetSelectorType type);

        PopUp ForType(PopUpType type);
    }
}