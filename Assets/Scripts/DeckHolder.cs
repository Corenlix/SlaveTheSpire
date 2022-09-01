
using System.Collections.Generic;
using System.Linq;
using Infrastructure.StaticData.Cards;
using Utilities;

public class DeckHolder : IDeckHolder
{
    Queue<CardId> _drawPile = new();
    Queue<CardId> _discardPile = new();

    public DeckHolder()
    {
        _drawPile.Enqueue(CardId.WarriorAoe);
        _drawPile.Enqueue(CardId.WarriorEating);
        _drawPile.Enqueue(CardId.WarriorSalo);
        _drawPile.Enqueue(CardId.WarriorValor);
        _drawPile.Enqueue(CardId.WarriorDrinkBeer);
        _drawPile.Enqueue(CardId.WarriorMegaAttack);
        _drawPile.Enqueue(CardId.WarriorDamageLikeDefense);
    }

    public CardId GetCard()
    {
        if(IsDrawPileEmpty())
            Refresh();
        
        CardId card = _drawPile.Dequeue();
        return card;
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

    private List<CardId> GetAllCards()
    {
        var allCards = new List<CardId>();
        allCards.AddRange(_drawPile.ToList());
        allCards.AddRange(_discardPile.ToList());

        return allCards;
    }

    private bool IsDrawPileEmpty() => _drawPile.Count == 0;
}