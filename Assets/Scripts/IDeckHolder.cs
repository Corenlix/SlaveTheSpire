using System.Collections.Generic;
using Card;
using Infrastructure;
using Infrastructure.Factories;
using Infrastructure.StaticData;
using Infrastructure.StaticData.Cards;
using UnityEngine;

public interface IDeckHolder
{
    CardId GetCard();
    
    void PushCard(CardId card);
}