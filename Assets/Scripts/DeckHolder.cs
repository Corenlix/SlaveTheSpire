
using System.Collections.Generic;
using System.Linq;
using Infrastructure.StaticData.Cards;
using Utilities;

public class DeckHolder : IDeckHolder
{
    Queue<CardId> _drawPile = new();
    Queue<CardId> _discardPile = new();

    public DeckHolder(List<CardId> cards)
    {
        cards.Shuffle();
        _drawPile = new Queue<CardId>(cards);
    }

    public CardId GetCard()
    {
        if (_drawPile.TryDequeue(out CardId card))
            return card;

        Refresh();
        return _drawPile.Dequeue();
    }

    public void PushCard(CardId cardId)
    {
        _discardPile.Enqueue(cardId);
    }

    private void Refresh()
    {
        var allCards = GetAllCards();
        allCards.Shuffle();
        
        _drawPile = new Queue<CardId>(allCards);
        _discardPile = new Queue<CardId>();
    }

    public List<CardId> GetAllCards()
    {
        var allCards = new List<CardId>();
        allCards.AddRange(_drawPile.ToList());
        allCards.AddRange(_discardPile.ToList());

        return allCards;
    }
}