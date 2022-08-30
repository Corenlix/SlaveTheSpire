using System.Collections.Generic;
using Card;
using Infrastructure;
using Infrastructure.Factories;
using Infrastructure.StaticData;
using UnityEngine;

public interface IDeckHolder
{
    CardId GetCard();
    
    void PushCard(CardId card);
}