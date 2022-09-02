using Infrastructure.StaticData.Cards;

public interface IDeckHolder
{
    CardId GetCard();
    
    void PushCard(CardId card);
}