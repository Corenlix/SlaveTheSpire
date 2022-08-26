namespace Infrastructure
{
    public interface IStaticDataService
    {
        CardStaticData ForCard(CardId cardId);
    }
}