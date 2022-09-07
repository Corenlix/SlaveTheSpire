using System.Collections.Generic;
using Infrastructure.StaticData.Cards;

public interface IDeckHolder
{
    CardId GetCard();
    List<CardId> GetAllCards();
    void PushCard(CardId card);
}