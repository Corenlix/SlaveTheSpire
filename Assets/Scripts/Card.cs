using System.Collections.Generic;

public class Card
{
    private int _cost;
    private ICardAction _cardAction;

    public Card(int cost, ICardAction cardAction)
    {
        _cost = cost;
        _cardAction = cardAction;
    }
    
    public void Use(List<Entity> targets)
    {
        targets.ForEach(_cardAction.Activate);
    }
}