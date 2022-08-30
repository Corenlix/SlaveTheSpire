
using System.Collections.Generic;
using Infrastructure.StaticData;
using Infrastructure.StaticData.Cards;
using UnityEngine;

public class DeckHolder : IDeckHolder
{
    Queue<CardId> _drawPile = new();
    Queue<CardId> _discardPile = new();

    public DeckHolder()
    {
        _drawPile.Enqueue(CardId.Damage);
        _drawPile.Enqueue(CardId.Damage);
        _drawPile.Enqueue(CardId.Damage);
        _drawPile.Enqueue(CardId.Damage);
        _drawPile.Enqueue(CardId.Test);
        _drawPile.Enqueue(CardId.TestBuff);
        _drawPile.Enqueue(CardId.Damage);
        _drawPile.Enqueue(CardId.Damage);
        _drawPile.Enqueue(CardId.Damage);
        _drawPile.Enqueue(CardId.Damage);

    }

    public CardId GetCard()
    {
        if(IsDrawPileEmpty())
            Refresh();
        
        var card = _drawPile.Dequeue();
        return card;
    }

    public void PushCard(CardId cardId)
    {
        _discardPile.Enqueue(cardId);
        Debug.Log($"{cardId} push to discardPile, discardPile.Count = {_discardPile.Count}");
    }

    private void Refresh()
    {
        _drawPile = _discardPile;
        _discardPile = new Queue<CardId>();
        Debug.Log($"drawPile.Count = {_drawPile.Count}, discardPile.Count = {_discardPile.Count}");
    }

    private bool IsDrawPileEmpty() => _drawPile.Count == 0;
}