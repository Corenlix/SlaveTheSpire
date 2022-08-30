
using System.Collections.Generic;
using Infrastructure.StaticData;
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
    }

    private void Refresh()
    {
        _drawPile = _discardPile;
        _discardPile = new Queue<CardId>();
    }

    private bool IsDrawPileEmpty() => _drawPile.Count == 0;
}